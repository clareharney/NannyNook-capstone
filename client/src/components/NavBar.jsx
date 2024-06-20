import { useState } from "react";
import { NavLink as RRNavLink } from "react-router-dom";
import {
Button,
Collapse,
Nav,
NavLink,
NavItem,
Navbar,
NavbarBrand,
NavbarToggler,
} from "reactstrap";
import { logout } from "../managers/authManagers.js";
import "./NavBar.css"


export default function NavBar({ loggedInUser, setLoggedInUser }) {
const [open, setOpen] = useState(false);

const toggleNavbar = () => setOpen(!open);

return (
    <div>
    <Navbar color="light" light fixed="true" expand="lg">
        <NavbarBrand className="mr-auto" tag={RRNavLink} to="/">
        NannyNook
        </NavbarBrand>
        {loggedInUser ? (
        <>
            <NavbarToggler onClick={toggleNavbar} />
            <Collapse isOpen={open} navbar>
            <Nav navbar>
                <NavItem>
                    <NavLink tag={RRNavLink} to="/home">
                        Home
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={RRNavLink} to="/myprofile">
                        My Profile
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={RRNavLink} to="/myevents">
                        My Events
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={RRNavLink} to="/events/create">
                        Create An Event
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={RRNavLink} to="/myrsvps">
                        My RSVP'd Events
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={RRNavLink} to="/jobs">
                        Job Postings
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={RRNavLink} to="/myjobs">
                        My Jobs
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={RRNavLink} to="/jobs/create">
                        Create A Job Posting
                    </NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={RRNavLink} to="/resources">
                        Child Resources
                    </NavLink>
                </NavItem>
            </Nav>
            </Collapse>
            <Button
            color="primary"
            onClick={(e) => {
                e.preventDefault();
                setOpen(false);
                logout().then(() => {
                setLoggedInUser(null);
                setOpen(false);
                });
            }}
            >
            Logout
            </Button>
        </>
        ) : (
        <Nav navbar>
            <NavItem>
            <NavLink tag={RRNavLink} to="/login">
                <Button color="primary">Login</Button>
            </NavLink>
            </NavItem>
        </Nav>
        )}
    </Navbar>
    </div>
);
}