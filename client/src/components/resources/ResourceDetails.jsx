import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import {
    Card,
    CardBody,
    CardTitle,
    CardText,
} from "reactstrap";
import { getResourceById } from "../../managers/resourceManager.js";
import "./ResourceDetails.css"; // Import the CSS file for styling

const ResourceDetails = () => {
    const [resource, setResource] = useState({});
    const { id } = useParams();

    useEffect(() => {
        getResourceById(id).then((obj) => {
            setResource(obj);
        });
    }, [id]);

    return (
        <div className="resource-details-container">
            <Card className="resource-card">
                <CardBody>
                    <CardTitle tag="h2">{resource.title}</CardTitle>
                    <CardText className="resource-description">{resource.description}</CardText>
                    <CardText><strong>Type:</strong> {resource.type}</CardText>
                    <CardText><strong>Author:</strong> {resource.author}</CardText>
                    {resource.url && (
                        <CardText>
                            <strong>URL:</strong>{" "}
                            <a href={resource.url} target="_blank" rel="noopener noreferrer">
                                {resource.url}
                            </a>
                        </CardText>
                    )}
                </CardBody>
            </Card>
        </div>
    );
};

export default ResourceDetails;
