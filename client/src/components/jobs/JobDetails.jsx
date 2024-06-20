import { useEffect, useState } from "react";
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
import { deleteJob, getJobById } from "../../managers/jobManager.js";

const JobDetails = ({ loggedInUser }) => {
    const [job, setJob] = useState({});
    const [modal, setModal] = useState(false);
    const [jobIdToDelete, setJobIdToDelete] = useState({});
    const [showConfirmation, setShowConfirmation] = useState(false);
    const toggle = () => setModal(!modal);
    const { id } = useParams();
    const navigate = useNavigate();

    useEffect(() => {
        getJobById(id).then((obj) => {
            setJob(obj);
        });
    }, [id]);

    const handleDeleteJob = async (jobId) => {
        try {
            await deleteJob(jobId).then(() => {
                navigate("/myjobs");
            });
        } catch (error) {
            console.error("Error deleting this post:", error);
        }
    };

    const handleConfirmDelete = () => {
        handleDeleteJob(jobIdToDelete);
        setShowConfirmation(false);
    };

    const handleCancelDelete = () => {
        setShowConfirmation(false);
    };
  

    return (
        <>
            <Card
                key={job.id}
                style={{
                    width: "25rem",
                }}
            >
                <CardBody>
                    <CardTitle tag="h5">{job.title}</CardTitle>
                    <CardText className="mb-2 text-muted" tag="h6">
                    </CardText>
                    <CardText>{job.description}</CardText>
                    <CardText>{`Pay: ${job.payRateMin}-${job.payRateMax} an hour`}</CardText>
                    <CardText>{`Number of kids: ${job.numberOfKids}`}</CardText>
                    <CardText>{`add full time ternary here`}</CardText>
                    <CardText>{`Contact ${job.contactInformation} for more information`}</CardText>
                    {/* <CardText>{`Posted By: ${job.poster.fullName}`}</CardText> */}
                    {job.posterId === loggedInUser.id ? (
                        <>
                            <div className="post-btns">
                                <Button onClick={() => navigate(`/myjobs/edit/${job.id}`)}>
                                    Edit
                                </Button>
                                <Button
                                    onClick={() => {
                                        setJobIdToDelete(job.id);
                                        setShowConfirmation(true);
                                    }}
                                >
                                    Delete
                                </Button>
                            </div>
                            {showConfirmation && (
                                <div className="confirmation-modal">
                                    <p>Are you sure you want to delete this job posting?</p>
                                    <Button onClick={handleConfirmDelete}>Delete</Button>
                                    <Button onClick={handleCancelDelete}>Cancel</Button>
                                </div>
                            )}
                        </>
                    ) : ("")}
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

export default JobDetails;
