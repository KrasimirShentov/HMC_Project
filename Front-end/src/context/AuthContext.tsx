// src/context/AuthContext.tsx

import React, { createContext, useState, useContext, useEffect, ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api'; // Assuming you have a pre-configured Axios instance
import { LoginCredentials, RegisterCredentials } from '../types/Types'; // Make sure these types are defined

// Define the shape of the context value
interface AuthContextType {
  isLoggedIn: boolean;
  login: (credentials: LoginCredentials) => Promise<void>;
  logout: () => void;
  register: (credentials: RegisterCredentials) => Promise<void>; // The new register function
}

// Create the context
const AuthContext = createContext<AuthContextType | undefined>(undefined);

// Create the AuthProvider component
export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(() => {
    const token = localStorage.getItem('token');
    // If a token exists, set the default authorization header for all subsequent API requests
    if (token) {
      api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    }
    return !!token;
  });
  const navigate = useNavigate();

  // A helper function to handle successful authentication
  const handleAuthentication = (token: string) => {
    localStorage.setItem('token', token);
    api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    setIsLoggedIn(true);
    navigate('/companies'); // Redirect to a protected page
  };

  // Login function
  const login = async (credentials: LoginCredentials) => {
    const response = await api.post('/user/login', credentials);
    const { token } = response.data;
    if (token) {
      handleAuthentication(token);
    } else {
      throw new Error("Login failed: No token received.");
    }
  };
  
  // NEW Register function
  const register = async (credentials: RegisterCredentials) => {
    // The backend now returns a token on successful registration
    const response = await api.post('/user/register', credentials);
    const { token } = response.data;
    if (token) {
      handleAuthentication(token);
    } else {
      throw new Error("Registration failed: No token received.");
    }
  };

  // Logout function
  const logout = () => {
    localStorage.removeItem('token');
    delete api.defaults.headers.common['Authorization'];
    setIsLoggedIn(false);
    navigate('/login'); // Redirect to login on logout
  };

  const value = { isLoggedIn, login, logout, register };

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  );
};

// Custom hook to use the auth context easily in other components
export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};