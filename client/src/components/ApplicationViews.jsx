import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute.jsx";
import Login from "./auth/Login";
import Register from "./auth/Register";
import CreateOccasion from "./occasions/CreateOccasion.jsx";
import { MyProfile } from "./profile/MyProfile.jsx";

export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <p>Welcome to NannyNook!</p>
            </AuthorizedRoute>
          }
        />
        <Route path="/events">
          <Route index element={<p>Where All Occasions in user's area will be located</p>} />
          <Route
            path="create"
            element={<CreateOccasion loggedInUser={loggedInUser} />}
          />
          <Route path=":id">
            <Route
              index
              element={<p>Occasion Details will go here</p>}
            />
          </Route>
          
        </Route>
        <Route
          path="myprofile"
          element={<MyProfile loggedInUser={loggedInUser} />}
        />
        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
