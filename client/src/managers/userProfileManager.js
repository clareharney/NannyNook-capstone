const _apiUrl = "/api/userprofile";

export const getProfiles = () => {
  return fetch(_apiUrl + "/withroles").then((res) => res.json());
};

export const getProfile = async (id) => {
  return await fetch(_apiUrl + `/${id}`).then((res) => res.json());
};

export const editProfile = async (profile, id) => {
  const response = await fetch(`${_apiUrl}/${id}`, {
    method: "PUT",
    headers: {
        "Content-Type": "application/json",
    },
    body: JSON.stringify(profile)
    })
    if (!response.ok) {
      const errorData = await response.json();
      console.error("Error updating profile:", errorData);
      throw new Error("Failed to update profile");
  }

  return response.json();
};
