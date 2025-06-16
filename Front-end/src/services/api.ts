// src/services/api.ts
import axios from "axios";

const api = axios.create({
  baseURL: 'http://localhost:5000',
  headers: {
    'Content-Type': 'application/json', // <--- Add this line
  },
});

export default api;queueMicrotask