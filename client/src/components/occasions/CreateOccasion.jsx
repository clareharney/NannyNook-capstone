import React, { useEffect, useState } from "react";
import DatePicker from "react-datepicker";
import 'react-datepicker/dist/react-datepicker.css';
import { Button, Form, FormGroup, Input, Label } from "reactstrap";
import { getAllCategories } from "../../managers/categoryManager.js";
import { useNavigate } from "react-router-dom";
import { createOccasion } from "../../managers/occasionManager.js";
import "./CreateOccasion.css"; // Adjusted CSS import

const CreateOccasion = ({ loggedInUser }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [state, setState] = useState("");
  const [city, setCity] = useState("");
  const [eventLocation, setEventLocation] = useState("");
  const [eventDate, setEventDate] = useState(null);
  const [category, setCategory] = useState(0);
  const [categories, setCategories] = useState([]);
  const [occasionImage, setOccasionImage] = useState("");

  const navigate = useNavigate();



  const handleSubmit = async (e) => {
    e.preventDefault();
    const newOccasion = {
      Title: title,
      Description: description,
      City: city,
      State: state,
      Location: eventLocation,
      Date: eventDate.toISOString(),
      CategoryId: category,
      HostUserProfileId: loggedInUser.id,
      OccasionImage: occasionImage
    };

    console.log("Creating new occasion with payload:", newOccasion);

    try {
      const createdOccasion = await createOccasion(newOccasion);
      navigate(`/events/${createdOccasion.id}`);
    } catch (error) {
      console.error("Error creating this event:", error);
    }
  };

  useEffect(() => {
    getAllCategories().then(setCategories);
  }, []);

  return (
    <div className="form-container">
      <h2 className="form-title">Create An Event!</h2>
      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label>Title</Label>
          <Input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup>
          <Label>Description</Label>
          <Input
            type="text"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup>
          <Label>State</Label>
          <Input
            type="text"
            value={state}
            onChange={(e) => setState(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup>
          <Label>City</Label>
          <Input
            type="text"
            value={city}
            onChange={(e) => setCity(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup>
          <Label>Event Location</Label>
          <Input
            type="text"
            value={eventLocation}
            onChange={(e) => setEventLocation(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup>
          <Label>Event Image URL</Label>
          <Input
            type="text"
            value={occasionImage}
            onChange={(e) => setOccasionImage(e.target.value)}
            className="form-input"
          />
        </FormGroup>
        <FormGroup>
          <Label>Date</Label>
          <div>
            <DatePicker
              selected={eventDate}
              onChange={(date) => setEventDate(date)}
              showTimeSelect
              timeFormat="hh:mm aa"
              timeIntervals={15}
              timeCaption="Time"
              dateFormat="MMMM d, yyyy h:mm aa"
              className="form-input"
              timeZone="CST"
            />
          </div>
        </FormGroup>
        <FormGroup>
          <Label>Category</Label>
          <Input
            type="select"
            value={category}
            onChange={(e) => setCategory(parseInt(e.target.value))}
            className="form-input"
          >
            <option value={0}>Choose a Category</option>
            {categories.map((c) => (
              <option key={c.id} value={c.id}>{c.name}</option>
            ))}
          </Input>
        </FormGroup>
        <Button type="submit" className="form-submit-button">Submit</Button>
      </Form>
    </div>
  );
};

export default CreateOccasion;
