import axios from "axios";

export default axios.create({
  baseURL: "https://localhost:7011/api/Task",
});
