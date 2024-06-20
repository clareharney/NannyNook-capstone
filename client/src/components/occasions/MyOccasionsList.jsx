import { useState, useEffect } from "react";
import { getOccasions } from "../../managers/occasionManager.js";
import { Button, Card, CardBody, CardSubtitle, CardText, CardTitle } from "reactstrap";
import { useNavigate } from "react-router-dom";

const MyOccasionsList = ({ loggedInUser}) => {
    const [occasions, setOccasions] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchData = async () => {
          try {
            const occasionsData = await getOccasions();
            const userOccasions = occasionsData
              .filter((occasion) => occasion.hostUserProfileId === loggedInUser.id)
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
                    <Card
                        key={o.id}
                        style={{
                            width: "10rem",
                        }}
                    >
                        <CardBody>
                            <CardTitle tag="h5">{o.title}</CardTitle>
                            <CardSubtitle className="mb-2 text-muted" tag="h6">
                                {o.hostUserProfile.fullName}
                            </CardSubtitle>
                            <CardText>{o.category?.name}</CardText>
                            <CardText>{o.location}</CardText>
                            <CardText>{o.formattedDate}</CardText>
                            <CardBody>{o.description}</CardBody>
                            <Button
                                onClick={() => {
                                    navigate(`/events/${o.id}`);
                                }}
                            >
                                View Occasion
                            </Button>
                        </CardBody>
                    </Card>
                ))
            ) : (
                <div>
                    <p>You haven't created an event yet!</p>
                    <Button onClick={() => navigate("/events/create")}>
                        Create an Event
                    </Button>
                </div>
            )}
        </div>
    );
}

export default MyOccasionsList
