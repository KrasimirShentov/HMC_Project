import React, { createContext, useState, useContext, useEffect, useCallback, useMemo, ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';
import { LoginCredentials, RegisterCredentials, GenderType } from '../types/Types';
import axios from 'axios';

interface AuthContextType {
    isLoggedIn: boolean;
    login: (credentials: LoginCredentials) => Promise<void>;
    logout: () => void;
    register: (credentials: RegisterCredentials) => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            setIsLoggedIn(true);
        } else {
            setIsLoggedIn(false);
        }
    }, []);

    const handleAuthentication = useCallback((token: string) => {
        localStorage.setItem('token', token);
        api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        setIsLoggedIn(true);
        navigate('/companies');
    }, [navigate]);

    const login = useCallback(async (credentials: LoginCredentials) => {
    console.log("AuthContext.tsx login received credentials:", credentials);
    try {
      const response = await api.post('/user/login', credentials);

        console.log("Sending Login Payload:", credentials); 
        const { token } = response.data;
        if (token) {
            handleAuthentication(token);
        } else {
                throw new Error("Login failed: No token received from server.");
        }
    } catch (error) {
        if (axios.isAxiosError(error)) {
            const errorMessage = error.response?.data?.title || error.response?.data?.message || JSON.stringify(error.response?.data) || error.message;
            console.error("Login API Error:", error.response?.data || error.message);
            throw new Error(`Login failed: ${JSON.stringify(errorMessage)}`);
        } else {
            console.error("An unexpected error occurred during login:", error);
            throw new Error("An unexpected error occurred during login.");
        }
    }
}, [handleAuthentication]);

    const register = useCallback(async (credentials: RegisterCredentials) => {
        try {
            const response = await api.post('/user/register', credentials);

            const { token } = response.data;
            if (token) {
                handleAuthentication(token);
            } else {
                throw new Error("Registration failed: No token received from server.");
            }
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.error("Registration API Error:", error.response?.data || error.message);
                if (error.response && error.response.data) {
                    throw error.response.data;
                } else {
                    throw new Error(error.message);
                }
            } else {
                console.error("An unexpected error occurred during registration:", error);
                throw new Error("An unexpected error occurred during registration.");
            }
        }
    }, [handleAuthentication]);

    const logout = useCallback(() => {
        localStorage.removeItem('token');
        delete api.defaults.headers.common['Authorization'];
        setIsLoggedIn(false);
        navigate('/login');
    }, [navigate]);

    const value = useMemo(() => ({
        isLoggedIn,
        login,
        logout,
        register
    }), [isLoggedIn, login, logout, register]);

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};

// Export your useAuth hook as a named export
export const useAuth = () => {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};