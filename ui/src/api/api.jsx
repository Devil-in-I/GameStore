import axios from 'axios';
import { variables } from '../Variables';

const token = localStorage.getItem("authToken");
const api = axios.create({
  headers: {
    
    Authorization: `Bearer ${token}`
  },
  baseURL: variables.API_URI,
});

// Интерцептор для обработки ошибок аутентификации
api.interceptors.response.use(
  response => response,
  function (error) {
    if (error.response && error.response.status === 401) {
      return Promise.reject(new Error('Unauthorized'));
    }
    return Promise.reject(error);
  }
);

export default api;
