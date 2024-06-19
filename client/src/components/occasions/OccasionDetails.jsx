import { useEffect, useState } from "react";
import { getOccasionById, deleteOccasion } from "../../managers/occasionManager.js";
import { Link, useParams, useNavigate } from "react-router-dom";
import {
    Card,
    CardBody,
    CardTitle,
    CardSubtitle,
    CardText,
    CardFooter,
    Button,
    Modal,
    ModalHeader,
    ModalBody,
    ModalFooter,
    Form,
    FormGroup,
    Label,
    Input,
} from "reactstrap";
import { getRSVPs, NewRSVP, UnRSVP } from "../../managers/RSVPManager.js";

const OccasionDetails = ({ loggedInUser }) => {
    const [occasion, setOccasion] = useState({});
    const [modal, setModal] = useState(false);
    const [occasionToDelete, setOccasionToDelete] = useState({});
    const [showConfirmation, setShowConfirmation] = useState(false);
    const [rsvps, setRsvps] = useState([]);
    const [userRsvps, setUserRsvps] = useState(false);
    const [rsvpCount, setRsvpCount] = useState(0);
    const { id } = useParams();
    const toggle = () => setModal(!modal);
    const navigate = useNavigate();

    useEffect(() => {
        getOccasionById(id).then((obj) => {
            setOccasion(obj);
            setRsvpCount(obj.rsvPs.length);
        });
    }, [id]);

    const refresh = () => {
        getOccasionById(id).then((obj) => {
            setOccasion(obj);
            setRsvpCount(obj.rsvPs.length);
        });
    };

    const formatDate = (dateString) => {
        if (!dateString) return "";
        const date = new Date(dateString);
        const options = { year: "numeric", month: "2-digit", day: "2-digit" };
        return new Intl.DateTimeFormat("en-US", options).format(date);
    };

    const handleDeleteOccasion = async (occasionId) => {
        try {
            await deleteOccasion(occasionId).then(() => {
                navigate("/myevents");
            });
        } catch (error) {
            console.error("Error deleting this post:", error);
        }
    };

    const handleConfirmDelete = () => {
        handleDeleteOccasion(occasionToDelete);
        setShowConfirmation(false);
    };

    const handleCancelDelete = () => {
        setShowConfirmation(false);
    };

    const handleRSVP = async () => {
      const RSVP = {
          hostUserProfileId: occasion?.hostUserProfile?.id,
          userProfileId: loggedInUser.id,
          occasionId: occasion.id, // Ensure you pass the occasion ID here
      };
      try {
          await NewRSVP(RSVP);
          refresh();
      } catch (error) {
          console.error("Failed to RSVP:", error);
          alert(error.message); // Display the error message
      }
  };
  
  const handleUnRSVP = async () => {
      const RSVP = {
          userProfileId: loggedInUser.id,
          occasionId: occasion.id,
      };
  
      try {
          await UnRSVP(RSVP.userProfileId, RSVP.occasionId);
          refresh();
      } catch (error) {
          console.error("Failed to un-RSVP:", error);
          alert(error.message); // Display the error message
      }
  };
  

    useEffect(() => {
        getRSVPs().then(setRsvps);
    }, [occasion]);

    useEffect(() => {
        const userRSVPs = occasion.rsvPs?.filter(
            (r) => r.userProfileId === loggedInUser.id && !r.endDate
        );

        setUserRsvps(userRSVPs?.length > 0);
    }, [occasion, loggedInUser.id]);

    return (
        <>
            <Card
                key={id}
                style={{
                    width: "25rem",
                }}
            >
                <img alt="Sample" src={occasion.occasionImage} />
                <CardBody>
                    <CardTitle tag="h5">{occasion.title}</CardTitle>
                    <CardSubtitle className="mb-2 text-muted" tag="h6">
                        <Link
                            to={
                                occasion.hostUserProfileId !== loggedInUser.id
                                    ? `/occasions/${occasion.hostUserProfile?.id}/${occasion.hostUserProfile?.identityUser?.userName}`
                                    : null
                            }
                        >
                            {occasion.hostUserProfile?.identityUser?.userName}
                        </Link>
                    </CardSubtitle>
                    <CardText>{occasion.description}</CardText>
                    <CardText>{formatDate(occasion.date)}</CardText>
                    <CardText>{occasion.location}</CardText>
                    {occasion.hostUserProfileId === loggedInUser.id ? (
                        <>
                            <CardText>RSVP Count: {rsvpCount}</CardText>
                            <div className="post-btns">
                                <Button onClick={() => navigate(`/myevents/edit/${occasion.id}`)}>
                                    Edit Event
                                </Button>
                                <Button
                                    onClick={() => {
                                        setOccasionToDelete(occasion.id);
                                        setShowConfirmation(true);
                                    }}
                                >
                                    Delete
                                </Button>
                            </div>
                            {showConfirmation && (
                                <div className="confirmation-modal">
                                    <p>Are you sure you want to delete this event?</p>
                                    <Button onClick={handleConfirmDelete}>Delete</Button>
                                    <Button onClick={handleCancelDelete}>Cancel</Button>
                                </div>
                            )}
                        </>
                    ) : (
                        <div>
                            {!userRsvps ? (
                                <Button
                                    className="post-btns"
                                    onClick={handleRSVP}
                                >
                                    RSVP
                                </Button>
                            ) : (
                                <Button
                                    className="post-btns"
                                    onClick={handleUnRSVP}
                                >
                                    Un-RSVP
                                </Button>
                            )}
                        </div>
                    )}
                </CardBody>
            </Card>
            <Modal isOpen={modal} toggle={toggle}>
                <ModalFooter>
                    <Button
                        color="primary"
                        onClick={() => handleSubmit(id, tagSelections)}
                    >
                        Save
                    </Button>{" "}
                    <Button color="secondary" onClick={toggle}>
                        Cancel
                    </Button>
                </ModalFooter>
            </Modal>
        </>
    );
};

export default OccasionDetails;
