import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { editOccasion, getOccasionById } from "../../managers/occasionManager.js";
import DatePicker from "react-datepicker"
import 'react-datepicker/dist/react-datepicker.css';
import { getAllCategories } from "../../managers/categoryManager.js";
import "./CreateOccasion.css"

const EditOccasion = ({loggedInUser}) => {
    const {occasionId} = useParams()
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [state, setState] = useState("")
    const [city, setCity] = useState("")
    const [eventLocation, setEventLocation] = useState("")
    const [eventDate, setEventDate] = useState(null)
    const [category, setCategory] = useState(0);
    const [categories, setCategories] = useState([]);
    const [occasionImage, setOccasionImage] = useState("");
    const [occasion, setOccasion] = useState({});
    const [selectedFile, setSelectedFile] = useState(null);

    const navigate = useNavigate()

    


    useEffect(() => {
      if (occasionId) {
        const fetchOccasion = async () => {
          try {
            const occasionData = await getOccasionById(occasionId)
            setOccasion(occasionData)
            setTitle(occasionData.title)
            setDescription(occasionData.description)
            setState(occasionData.state)
            setCity(occasionData.city)
            setEventLocation(occasionData.location)
            setEventDate(new Date(occasionData.date))
            setCategory(occasionData.categoryId)
            setOccasionImage(occasionData.occasionImage)
            } catch (error) {
              console.error("Error fetching this occasion:", error)
              }
            }
            
            fetchOccasion()
            }
            }, [occasionId])
            
            useEffect(() => {
              getAllCategories().then(setCategories);
              }, []);
              
              const handleSave = (e) => {
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
        }

        console.log('Occasion Data:', occasionData)
        
        try {
          const response = editOccasion(occasionData, parseInt(occasionId));
          console.log('Response:', response);  // Debug logging
          navigate(`/events/${occasionId}`)
        } catch (error) {
          console.error("There was an error saving the occasion!", error);
        }
      };

      const handleCancel = () => {
        navigate(`/events/${postId}`);
      };

      return (
        <>
          <h2 className="form-title">Edit Event</h2>
          <Form onSubmit={handleSave} className="form-container">
            <FormGroup>
              <Label>Title</Label>
              <Input
                className="form-input"
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
                className="form-input"
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
                className="form-input"
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
                className="form-input"
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
                className="form-input"
                type="text"
                value={eventLocation}
                onChange={(e) => {
                  setEventLocation(e.target.value);
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label>Date</Label>
              <div className="form-input">
                <DatePicker
                    selected={eventDate}
                    onChange={(date) => setEventDate(date)}
                    showTimeSelect
                    timeFormat="hh:mm aa"
                    timeIntervals={15}
                    timeCaption="Time"
                    dateFormat="MMMM d, yyyy h:mm aa"
                    timeZone="local"
                />
              </div>
            </FormGroup>
            <FormGroup>
              <Label>Category</Label>
              <Input
                className="form-input"
                type="select"
                value={category}
                onChange={(e) => {
                  setCategory(parseInt(e.target.value));
                }}
              >
                <option value={0}>Choose a New Category</option>
                {categories.map((c) => (
                  <option key={c.id} value={c.id}>{`${c.name}`}</option>
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
}

export default EditOccasion

