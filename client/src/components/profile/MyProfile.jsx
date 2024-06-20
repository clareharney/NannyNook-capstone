import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getProfile } from "../../managers/userProfileManager";
import { Card, CardBody, CardTitle, Button } from "reactstrap";
import "./MyProfile.css"

export const MyProfile = ({ loggedInUser }) => {
  const [userProfile, setUserProfile] = useState();
  const { id } = useParams();

  const navigate = useNavigate();

  useEffect(() => {
    getProfile(loggedInUser.id).then(setUserProfile);
  }, [id, loggedInUser.id]);

  if (!userProfile) {
    return null;
  }

  return (
    <>
      <div className="profile-container">
        <Card className="profile-card">
          <img
            alt={userProfile.firstName}
            src={userProfile.profileImage}
            className="profile-image"
          />
          <CardBody>
            <CardTitle tag="h5">Name: {userProfile.fullName}</CardTitle>
            <CardTitle tag="h5">Username: {userProfile.userName}</CardTitle>
            <CardTitle tag="h5">Email: {userProfile.email}</CardTitle>
            <CardTitle tag="h5">Location: {userProfile.location}</CardTitle>
            <CardTitle tag="h5">
              Bio: {userProfile.bio === null ? "No bio yet!" : userProfile.bio}
            </CardTitle>
            <Button
              className="edit-button"
              onClick={() => {
                navigate(`/myprofile/edit/${loggedInUser.id}`);
              }}
            >
              Edit
            </Button>
          </CardBody>
        </Card>
      </div>
    </>
  );
};
