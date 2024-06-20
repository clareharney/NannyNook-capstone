import React, { useState, useEffect } from "react";
import { getOccasions } from "../../managers/occasionManager.js";
import { Button, Card, CardBody, CardSubtitle, CardText, CardTitle } from "reactstrap";
import { useNavigate } from "react-router-dom";
import "./MyOccasionsList.css";

const MyOccasionsList = ({ loggedInUser }) => {
  const [occasions, setOccasions] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const occasionsData = await getOccasions();
        const userOccasions = occasionsData.filter(
          (occasion) => occasion.hostUserProfileId === loggedInUser.id
        );
        setOccasions(userOccasions);
      } catch (error) {
        console.error("Error fetching:", error);
      }
    };

    fetchData();
  }, [loggedInUser]);

  return (
    <div className="container">
      <h1>My Events</h1>
      {occasions.length > 0 ? (
        occasions.map((o) => (
          <Card className="card" key={o.id}>
            <CardBody className="card-body">
              <CardTitle className="card-title" tag="h5">
                {o.title}
              </CardTitle>
              <CardSubtitle className="card-subtitle" tag="h6">
                {o.hostUserProfile.fullName}
              </CardSubtitle>
              <CardText className="card-text">{o.category?.name}</CardText>
              <CardText className="card-text">{o.location}</CardText>
              <CardText className="card-text">{o.formattedDate}</CardText>
              <CardText className="card-description">{o.description}</CardText>
              <div className="card-buttons">
                <Button
                  color="primary"
                  onClick={() => {
                    navigate(`/events/${o.id}`);
                  }}
                >
                  View Event
                </Button>
              </div>
            </CardBody>
          </Card>
        ))
      ) : (
        <div>
          <p className="notif">You haven't created an event yet!</p>
          <Button onClick={() => navigate("/events/create")}>
            Create an Event
          </Button>
        </div>
      )}
    </div>
  );
};

export default MyOccasionsList;
