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

const OccasionDetails = ({loggedInUser}) => {
    const [occasion, setOccasion] = useState({})
    const [modal, setModal] = useState(false)
    const [occasionToDelete, setOccasionToDelete] = useState({})
    const [showConfirmation, setShowConfirmation] = useState();
    const [rsvps, setRsvps] = useState([])
    const [userRsvps, setUserRsvps] = useState(false)
    const { id } = useParams()
    const toggle = () => setModal(!modal)
    const navigate = useNavigate()

    useEffect(() => { 
        getOccasionById(id).then((obj) => setOccasion(obj))
    }, [id, occasion.rsvps])

    const refresh = () => {
        getOccasionById(id).then((obj) => setOccasion(obj))
    }

    const formatDate = (dateString) => {
        if (!dateString) return "";
        const date = new Date(dateString);
        const options = { year: "numeric", month: "2-digit", day: "2-digit" };
        return new Intl.DateTimeFormat("en-US", options).format(date);
      };
      
  const handleDeletePost = async (postId) => {
    try {
      await deletePost(postId).then(() => {
        navigate("/myposts");
      });
    } catch (error) {
      console.error("Error deleting this post:", error);
    }
  };

  const handleConfirmDelete = () => {
    handleDeletePost(postToDelete);
    setShowConfirmation(false);
  };

  const handleCancelDelete = () => {
    setShowConfirmation(false);
  };

  const handleRSVP = async () => {
    const RSVP = {
      hostUserProfileId: occasion?.hostUserProfile?.id,
      followerId: loggedInUser.id,
    };
    await NewSubscription(subscription);
    refresh();
  };

}
export default OccasionDetails