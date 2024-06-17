import { useEffect, useState } from "react";
import DatePicker from "react-datepicker"
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import { getAllCategories } from "../../managers/categoryManager.js";
import { useNavigate } from "react-router-dom";
import { createPost } from "../../managers/postManager.js";

const CreateOccasion = ({ loggedInUser }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [state, setState] = useState("")
  const [city, setCity] = useState("")
  const [eventLocation, setEventLocation] = useState("")
  const [eventDate, setEventDate] = useState(null)
  const [category, setCategory] = useState(0);
  const [categories, setCategories] = useState([]);

  const navigate = useNavigate();

  
  const handleSubmit = async (e) => {

    e.preventDefault();
    const newOccasion = {
    Title: title,
    Description: description,
    City: city,
    State: state,
    Location: eventLocation,
    Date: eventDate,
    CategoryId: category,
    HostUserProfileId: loggedInUser.id
    };

    console.log("Creating new occasion with payload:", newOccasion);

    try {
      const createdOccasion = await createPost(newOccasion);
      navigate(`/posts/${createdOccasion.id}`)
    }
    catch (error) {
      console.error("Error creating this event:" , error)
    }
  };

  useEffect(() => {
    getAllCategories().then(setCategories);
  }, []);

  return (
    <>
      <h2>Create An Event!</h2>
      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label>Title</Label>
          <Input
            type="text"
            value={title}
            onChange={(e) => {
              setTitle(e.target.value);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label>Description</Label>
          <Input
            type="text"
            value={description}
            onChange={(e) => {
              setDescription(e.target.value);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label>State</Label>
          <Input
            type="text"
            value={state}
            onChange={(e) => {
              setState(e.target.value);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label>City</Label>
          <Input
            type="text"
            value={city}
            onChange={(e) => {
              setCity(e.target.value);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label>Event Location</Label>
          <Input
            type="text"
            value={eventLocation}
            onChange={(e) => {
              setEventLocation(e.target.value);
            }}
          />
        </FormGroup>
        <FormGroup>
          <Label>Date</Label>
          <div>
            <DatePicker
              showTimeSelect
              selected={eventDate}
              onChange={(eventDate) => setEventDate(eventDate)}
            />
          </div>
        </FormGroup>
        <FormGroup>
          <Label>Category</Label>
          <Input
            type="select"
            value={category}
            onChange={(e) => {
              setCategory(parseInt(e.target.value));
            }}
          >
            <option value={0}>Choose a Category</option>
            {categories.map((c) => (
              <option key={c.id} value={c.id}>{`${c.categoryName}`}</option>
            ))}
          </Input>
        </FormGroup>
        <Button type="submit">Submit</Button>
      </Form>
    </>
  );
}

export default CreateOccasion;