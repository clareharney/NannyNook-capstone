const _apiUrl = "/api/occasion";

export const getOccasions = (search, categoryId, pending) => {
  let url = _apiUrl;
  if (pending == true) {
    url += "/pending";
  }
  if (search && categoryId) {
    url += `?search=${search}&categoryId=${categoryId}`;
  } else if (search) {
    url += `?search=${search}`;
  } else if (categoryId) {
    url += `?categoryId=${categoryId}`;
  }
  return fetch(url).then((res) => res.json());
};

export const getOccasionById = (id) => {
  return fetch(_apiUrl + `/${id}`).then((res) => res.json());
};

export const createOccasion = async (occasion) => {
  try {
    const response = await fetch(_apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(occasion),
    });
    if (!response.ok) {
      console.error(
        "Server response status:",
        response.status,
        response.statusText
      );
      try {
        const errorData = await response.json();
        console.error("Server response JSON:", errorData);
      } catch (jsonError) {
        console.error(
          "Server response could not be parsed as JSON:",
          jsonError
        );
      }
      throw new Error("Failed to create this occasion");
    }

    return await response.json();
  } catch (error) {
    console.error("Error creating this occasion:", error);
    throw error;
  }
};

export const deleteOccasion = (occasionId) => {
  return fetch(_apiUrl + `/${occasionId}`, {
    method: "DELETE",
  }).then((response) => {
    if (!response.ok) {
      throw new Error("Failed to delete occasion");
    }
  });
};

export const editOccasion = (formData, id) => {
  return fetch(`${_apiUrl}/${id}`, {
    method: "PUT",
    body: formData,
  }).then((res) => res.json());
};