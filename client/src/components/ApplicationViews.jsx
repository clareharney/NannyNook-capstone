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

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/" element={
        <AuthorizedRoute loggedInUser={loggedInUser}>
          <HomePage />
        </AuthorizedRoute>
      } />
      
      <Route path="events">
        <Route index element={<p>Where All Occasions in user's area will be located</p>} />
        <Route path="create" element={<CreateOccasion loggedInUser={loggedInUser} />} />
        <Route path=":id" element={<OccasionDetails loggedInUser={loggedInUser} />} />
      </Route>

      <Route path="myevents">
        <Route index element={ <MyOccasionsList loggedInUser={loggedInUser}/> } />
        <Route path="edit/:occasionId" element={
          <AuthorizedRoute loggedInUser={loggedInUser}>
            <EditOccasion loggedInUser={loggedInUser} />
          </AuthorizedRoute>
        } />
      </Route>

      {/* <Route path="myprofile" element={<MyProfile loggedInUser={loggedInUser} />} /> */}
      <Route path="login" element={<Login setLoggedInUser={setLoggedInUser} />} />
      <Route path="register" element={<Register setLoggedInUser={setLoggedInUser} />} />
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}