import React, { useState, useEffect } from "react";
import { Button, Card, CardBody, CardSubtitle, CardText, CardTitle } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { getRSVPs } from "../../managers/RSVPManager.js";
import "./RSVPdOccasions.css";

const RSVPdOccasions = ({ loggedInUser }) => {
  const [rsvps, setRsvps] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const rsvpsData = await getRSVPs();
        const userRSVPs = rsvpsData.filter(
          (rsvp) => rsvp.userProfileId === loggedInUser.id
        );
        setRsvps(userRSVPs);
      } catch (error) {
        console.error("Error fetching:", error);
      }
    };

    fetchData();
  }, [loggedInUser]);

  return (
    <div className="container">
      <h1>RSVP'd Events</h1>
      <div className="grid-container">
        {rsvps.map((r) => (
          <div className="grid-item" key={r.id}>
            <Card>
              <CardBody>
                <CardTitle tag="h5">{r.occasion.title}</CardTitle>
                <CardSubtitle className="mb-2 text-muted" tag="h6">
                  {r.occasion.hostUserProfileId}
                </CardSubtitle>
                <CardText>{`${r.occasion.city}, ${r.occasion.state}`}</CardText>
                <CardText>{r.occasion.location}</CardText>
                <CardText>{r.occasion.formattedDate}</CardText>
                <CardText>{r.occasion.description}</CardText>
                <Button
                  onClick={() => {
                    navigate(`/events/${r.occasion.id}`);
                  }}
                >
                  View Occasion
                </Button>
              </CardBody>
            </Card>
          </div>
        ))}
      </div>
    </div>
  );
};

export default RSVPdOccasions;
