import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { editProfile, getProfile } from "../../managers/userProfileManager";
import { Button, Form, FormGroup, Input, Label } from "reactstrap";

export const EditMyProfile = ({loggedInUser}) => {

  const [profileImage, setProfileImage] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [userName, setUsername] = useState("");
  const [bio, setBio] = useState("")
  const [location, setLocation] = useState("")
  const [profile, setProfile] = useState({});

  const navigate = useNavigate();


  useEffect(() => {
    getProfile(parseInt(loggedInUser.id)).then((foundProfile) => {
      setProfile(foundProfile);
      setFirstName(foundProfile.firstName);
      setLastName(foundProfile.lastName);
      setEmail(foundProfile.email);
      setUsername(foundProfile.userName);
      setBio(foundProfile.bio);
      setLocation(foundProfile.location);
      setProfileImage(foundProfile.profileImage)
    });
  }, [loggedInUser.id]);


  const handleSubmit = async (event) => {
    event.preventDefault();
    const updatedUser = {
        FirstName: firstName,
        LastName: lastName,
        Email: email,
        Username: userName,
        Bio: bio,
        Location: location,
        profileImage: profileImage
    }

    try {
      const response = await editProfile(updatedUser, parseInt(loggedInUser.id));
      console.log('Response:', response);  // Debug logging
      navigate('/myprofile')

    } catch (error) {
      console.error("There was an error uploading the file!", error);
    }
  };

  return (
    <>
    
    <Form onSubmit={handleSubmit}>
      <FormGroup>
        <Label>First Name:</Label>
        <Input
          type="text"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          />
      </FormGroup>
      <FormGroup>
        <Label>Last Name:</Label>
        <Input
          type="text"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          />
      </FormGroup>
      <FormGroup>
        <Label>Username:</Label>
        <Input
          type="text"
          value={userName}
          onChange={(e) => setUsername(e.target.value)}
        />
      </FormGroup>
      <FormGroup>
        <Label>Email Address:</Label>
        <Input
          type="text"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          />
      </FormGroup>
      <FormGroup>
        <Label>Located in:</Label>
        <Input
          type="text"
          value={location}
          onChange={(e) => setLocation(e.target.value)}
          />
      </FormGroup>
      <FormGroup>
        <Label>Bio:</Label>
        <Input
          type="text"
          value={bio}
          onChange={(e) => setBio(e.target.value)}
          />
      </FormGroup>
      <FormGroup>
        <Label>Profile Image:</Label>
        <Input
          type="text"
          value={profileImage}
          onChange={(e) => setProfileImage(e.target.value)}
          />
      </FormGroup>
      <Button type="submit">Submit</Button>
    </Form>
</>
  );
};