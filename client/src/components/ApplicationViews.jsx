import { Outlet, Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute.jsx";
import Login from "./auth/Login";
import Register from "./auth/Register";
import CreateOccasion from "./occasions/CreateOccasion.jsx";
import { MyProfile } from "./profile/MyProfile.jsx";
import OccasionDetails from "./occasions/OccasionDetails.jsx";
import EditOccasion from "./occasions/EditOccasion.jsx";
import MyOccasionsList from "./occasions/MyOccasionsList.jsx";
import HomePage from "./HomePage.jsx";
import { EditMyProfile } from "./profile/EditProfile.jsx";
import RSVPdOccasions from "./occasions/RSVPdOccasions.jsx";
import JobDetails from "./jobs/JobDetails.jsx";
import AllJobs from "./jobs/AllJobs.jsx";

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/" element={
        <AuthorizedRoute loggedInUser={loggedInUser}>
          <p>Welcome to NannyNook!!</p>
        </AuthorizedRoute>
      } />
      <Route path="home" element={
        <AuthorizedRoute loggedInUser={loggedInUser}>
          <HomePage loggedInUser={loggedInUser} />
        </AuthorizedRoute>
      } />
      <Route path="myprofile">
        <Route index element={<AuthorizedRoute loggedInUser={loggedInUser}>
          <MyProfile loggedInUser={loggedInUser}/>
        </AuthorizedRoute>} />
        <Route path="edit/:profileId" element={
          <AuthorizedRoute loggedInUser={loggedInUser}>
            <EditMyProfile loggedInUser={loggedInUser} />
          </AuthorizedRoute>
        } />
      </Route>
      <Route path="events">
        <Route index element={<AuthorizedRoute loggedInUser={loggedInUser}>
          <p>Placeholder</p>
        </AuthorizedRoute>} />
        <Route path="create" element={<AuthorizedRoute loggedInUser={loggedInUser}>
          <CreateOccasion loggedInUser={loggedInUser} />
        </AuthorizedRoute>} />
        <Route path=":id" element={<AuthorizedRoute loggedInUser={loggedInUser}>
          <OccasionDetails loggedInUser={loggedInUser} />
        </AuthorizedRoute>} />
      </Route>

      <Route path="myevents">
        <Route index element={<AuthorizedRoute loggedInUser={loggedInUser}>
          <MyOccasionsList loggedInUser={loggedInUser}/>
        </AuthorizedRoute>} />
        <Route path="edit/:occasionId" element={
          <AuthorizedRoute loggedInUser={loggedInUser}>
            <EditOccasion loggedInUser={loggedInUser} />
          </AuthorizedRoute>
        } />
      </Route>

      <Route path="jobs">
        <Route index element={<AuthorizedRoute loggedInUser={loggedInUser}>
          <AllJobs loggedInUser={loggedInUser} />
        </AuthorizedRoute>} />
        <Route path="create" element={<AuthorizedRoute loggedInUser={loggedInUser}>
          <p>Create A Job form will go here</p>
        </AuthorizedRoute>} />
        <Route path=":id" element={<AuthorizedRoute loggedInUser={loggedInUser}>
          <JobDetails loggedInUser={loggedInUser} />
        </AuthorizedRoute>} />
      </Route>

      <Route path="myrsvps" element={<RSVPdOccasions loggedInUser={loggedInUser} />} />
      <Route path="login" element={<Login setLoggedInUser={setLoggedInUser} />} />
      <Route path="register" element={<Register setLoggedInUser={setLoggedInUser} />} />
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
