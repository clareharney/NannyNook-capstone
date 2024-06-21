import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { editOccasion, getOccasionById } from "../../managers/occasionManager.js";
import DatePicker from "react-datepicker";
import 'react-datepicker/dist/react-datepicker.css';
import { getAllCategories } from "../../managers/categoryManager.js";
import "./CreateOccasion.css"; // Reusing the CreateOccasion.css styles

const EditOccasion = ({ loggedInUser }) => {
  const { occasionId } = useParams();
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [state, setState] = useState("");
  const [city, setCity] = useState("");
  const [eventLocation, setEventLocation] = useState("");
  const [eventDate, setEventDate] = useState(new Date());
  const [category, setCategory] = useState(0);
  const [categories, setCategories] = useState([]);
  const [occasionImage, setOccasionImage] = useState("");
  const [occasion, setOccasion] = useState({});
  const [selectedFile, setSelectedFile] = useState(null);

  const navigate = useNavigate();

  useEffect(() => {
    if (occasionId) {
      const fetchOccasion = async () => {
        try {
          const occasionData = await getOccasionById(occasionId);
          setOccasion(occasionData);
          setTitle(occasionData.Title);
          setDescription(occasionData.Description);
          setState(occasionData.State);
          setCity(occasionData.City);
          setEventLocation(occasionData.Location);
          setEventDate(new Date(occasionData.Date));
          setCategory(occasionData.CategoryId);
          setOccasionImage(occasionData.OccasionImage);
        } catch (error) {
          console.error("Error fetching this occasion:", error);
        }
      };

      fetchOccasion();
    }
  }, [occasionId]);

  useEffect(() => {
    getAllCategories().then(setCategories);
  }, []);

  const handleSave = async (e) => {
    e.preventDefault();
    const occasionData = {
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

    console.log("Occasion Data:", occasionData);

    try {
      const response = await editOccasion(occasionData, parseInt(occasionId));
      console.log('Response:', response);  // Debug logging
      navigate(`/events/${occasionId}`);
    } catch (error) {
      console.error("There was an error saving the occasion!", error);
    }
  };

  const handleCancel = () => {
    navigate(`/events/${occasionId}`);
  };

  return (
    <>
      <h2 className="form-title">Edit Event</h2>
      <Form onSubmit={handleSave} className="form-container">
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
            <option value={0}>Choose a New Category</option>
            {categories.map((c) => (
              <option key={c.id} value={c.id}>{c.name}</option>
            ))}
          </Input>
        </FormGroup>
        <Button color="primary" type="submit" className="form-submit-button">
          Save
        </Button>
        <Button color="secondary" onClick={handleCancel} className="form-submit-button">
          Cancel
        </Button>
      </Form>
    </>
  );
};

export default EditOccasion;
