import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getProfile } from "../../managers/userProfileManager";
import { Card, CardBody, CardTitle} from "reactstrap";

export const MyProfile = ({ loggedInUser }) => {
  const [userProfile, setUserProfile] = useState();
  const { id } = useParams();

  useEffect(() => {
    getProfile(loggedInUser.id).then(setUserProfile);
  }, [id]);

  if (!userProfile) {
    return null;
  }
  return (
    <>
      <Card
        key={id}
        style={{
          width: "25rem",
        }}
      >
        <img alt={userProfile.firstName} src={userProfile.profileImage} />
        <CardBody>
          <CardTitle tag="h5">Name: {userProfile.fullName}</CardTitle>
        </CardBody>
        <CardBody>
          <CardTitle tag="h5">Username: {userProfile.userName}</CardTitle>
        </CardBody>
        <CardBody>
          <CardTitle tag="h5">Email: {userProfile.email}</CardTitle>
        </CardBody>
        <CardBody>
          <CardTitle tag="h5">Location: {userProfile.location}</CardTitle>
        </CardBody>
        <CardBody>
          <CardTitle tag="h5">Bio: {userProfile.bio === null ? "No bio yet!" : userProfile.bio}</CardTitle>
        </CardBody>
      </Card>
    </>
  );
};
