// src/components/RegisterForm.tsx

import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { GenderType, RegisterCredentials } from '../types/Types'; // Ensure you have this type defined
import { Link } from 'react-router-dom';

const RegisterForm: React.FC = () => {
    const { register } = useAuth();
    const [formData, setFormData] = useState<RegisterCredentials>({
        name: '',
        surname: '',
        userName: '',
        email: '',
        password: '',
        gender: GenderType.Male, 
        dateOfBirth: '',
        addresses: [{ addressName: '' }],
    });
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleAddressChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        // This simple handler assumes only one address for simplicity.
        // You can expand this to handle multiple addresses if needed.
        setFormData(prev => ({
            ...prev,
            addresses: [{ addressName: e.target.value }],
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        // Basic validation
        if (formData.password.length < 6) {
             setError("Password must be at least 6 characters long.");
             setLoading(false);
             return;
        }

        try {
            // Use the register function from AuthContext
            await register(formData);
            // Navigation on success is handled inside the AuthContext
        } catch (err: any) {
            console.error("Registration error:", err);
            // Set error message from API response or a generic one
            setError(err.response?.data || err.message || "Registration failed. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="form-container">
            <h2 className="section-header">Register</h2>
            <form onSubmit={handleSubmit}>
                {error && <p className="error-text">{error}</p>}
                
                {/* Input fields for Name, Surname, UserName, Email, Password */}
                <div className="form-group">
                    <label htmlFor="name">First Name:</label>
                    <input type="text" id="name" name="name" value={formData.name} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label htmlFor="surname">Last Name:</label>
                    <input type="text" id="surname" name="surname" value={formData.surname} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label htmlFor="userName">Username:</label>
                    <input type="text" id="userName" name="userName" value={formData.userName} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label htmlFor="email">Email:</label>
                    <input type="email" id="email" name="email" value={formData.email} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label htmlFor="password">Password:</label>
                    <input type="password" id="password" name="password" value={formData.password} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label htmlFor="dateOfBirth">Date of Birth:</label>
                    <input type="date" id="dateOfBirth" name="dateOfBirth" value={formData.dateOfBirth} onChange={handleChange} required />
                </div>
                <div className="form-group">
                    <label htmlFor="address">Address:</label>
                    <input type="text" id="address" name="address" value={formData.addresses[0].addressName} onChange={handleAddressChange} required />
                </div>
                
                <div className="form-buttons">
                    <button type="submit" className="action-button primary" disabled={loading}>
                        {loading ? 'Registering...' : 'Register'}
                    </button>
                </div>
                <p style={{ textAlign: 'center', marginTop: '1rem' }}>
                    Already have an account? <Link to="/login">Log in here</Link>
                </p>
            </form>
        </div>
    );
};

export default RegisterForm;