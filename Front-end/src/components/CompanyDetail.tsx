// src/pages/CompanyDetailsPage.tsx
import React, { useEffect, useState, useCallback } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api'; // Assuming api.ts is in src/services
import { Company, CompanyUpdateDto } from '../types/Types'; // Assuming types are in src/types

import CompanyDepartmentsSection from '../components/CompanyDepartmentsSection'; // Keep this new component import
import styles from '../components/CSSComponents/CompanyDetail.module.css'; // Your CSS module location

const CompanyDetailsPage: React.FC = () => {
    const { companyId } = useParams<{ companyId: string }>();
    const navigate = useNavigate();

    const [company, setCompany] = useState<Company | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [isEditingCompany, setIsEditingCompany] = useState<boolean>(false);
    const [editCompanyFormData, setEditCompanyFormData] = useState<CompanyUpdateDto | null>(null);
    const [refreshTrigger, setRefreshTrigger] = useState<number>(0); // To trigger re-fetch after department changes

    const fetchCompanyDetails = useCallback(async () => {
        if (!companyId) {
            setError('Company ID is missing.');
            setLoading(false);
            return;
        }
        try {
            setLoading(true);
            setError(null);
            const response = await api.get<any>(`/Company/${companyId}`);
            const companyData = response.data;

            // Map the incoming data to your Company type, ensuring all nested arrays are handled
            const mappedCompany: Company = {
                id: companyData.id,
                name: companyData.name,
                description: companyData.description || '',
                email: companyData.email || '',
                phoneNumber: companyData.phoneNumber || '',
                addresses: companyData.addresses?.map((addr: any) => ({ addressName: addr.addressName })) || [],
                departments: companyData.departments?.map((dept: any) => ({
                    id: dept.id,
                    name: dept.name,
                    description: dept.description || '',
                    email: dept.email || '',
                    phoneNumber: dept.phoneNumber || '',
                    type: dept.type || '',
                    companyId: dept.companyID, // Ensure correct casing from backend
                    companyName: dept.companyName || '',
                    companyDescription: dept.companyDescription || '',
                    addresses: dept.addresses?.map((addr: any) => ({ addressName: addr.addressName })) || [],
                    employees: dept.employees?.map((emp: any) => ({
                        id: emp.id,
                        firstName: emp.firstName,
                        lastName: emp.lastName,
                        age: emp.age,
                        email: emp.email,
                        position: emp.position,
                        gender: emp.gender,
                        departmentId: dept.id,
                        trainingDetails: emp.trainingDto ? {
                            id: emp.trainingDto.id,
                            type: emp.trainingDto.type,
                            positionName: emp.trainingDto.positionName,
                            description: emp.trainingDto.description,
                            trainingHours: emp.trainingDto.trainingHours,
                        } : null,
                    })) || [],
                })) || [],
            };
            setCompany(mappedCompany);
            setEditCompanyFormData({
                name: mappedCompany.name,
                description: mappedCompany.description,
                email: mappedCompany.email,
                phoneNumber: mappedCompany.phoneNumber,
            });
        } catch (err: any) {
            console.error('Error fetching company details:', err);
            setError(err.response?.data || err.message || 'Failed to load company details.');
        } finally {
            setLoading(false);
        }
    }, [companyId, refreshTrigger]);

    useEffect(() => {
        fetchCompanyDetails();
    }, [fetchCompanyDetails]);

    const handleDepartmentChange = () => {
        // This callback is passed to CompanyDepartmentsSection to trigger a re-fetch
        // of company details when a department is added/updated/deleted.
        setRefreshTrigger(prev => prev + 1);
    };

    const handleEditCompanyClick = () => {
        setIsEditingCompany(true);
    };

    const handleCancelEditCompany = () => {
        setIsEditingCompany(false);
        if (company) {
            setEditCompanyFormData({
                name: company.name,
                description: company.description,
                email: company.email,
                phoneNumber: company.phoneNumber,
            });
        }
    };

    const handleCompanyFormChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setEditCompanyFormData(prev => ({
            ...prev!,
            [name]: value
        }));
    };

    const handleUpdateCompany = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!editCompanyFormData || !companyId) return;

        try {
            setLoading(true);
            setError(null);
            await api.put(`/Company/${companyId}`, editCompanyFormData);
            alert('Company updated successfully!');
            setIsEditingCompany(false);
            setRefreshTrigger(prev => prev + 1); // Trigger re-fetch to show updated data
        } catch (err: any) {
            console.error('Error updating company:', err);
            setError(err.response?.data || err.message || 'Failed to update company.');
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteCompany = async () => {
        if (!companyId) return;

        if (window.confirm('Are you sure you want to delete this company and all its associated departments and employees? This action cannot be undone.')) {
            try {
                setLoading(true);
                setError(null);
                await api.delete(`/Company/${companyId}`);
                alert('Company deleted successfully!');
                navigate('/companies'); // Redirect to companies list after deletion
            } catch (err: any) {
                console.error('Error deleting company:', err);
                setError(err.response?.data || err.message || 'Failed to delete company.');
            } finally {
                setLoading(false);
            }
        }
    };

    if (loading) {
        // Simple inline loading message
        return <p className={styles['loading-text']}>Loading company details...</p>;
    }

    if (error) {
        // Simple inline error message
        return <p className={styles['error-text']}>Error: {error}</p>;
    }

    if (!company) {
        // Simple inline "no data" message
        return <p className={styles['no-data-text']}>No company data available.</p>;
    }

    return (
        <div className={styles['company-detail-container']}>
            {isEditingCompany ? (
                <form onSubmit={handleUpdateCompany} className={styles['form-container']}>
                    <h2 className={styles['section-header']}>Edit Company: {company.name}</h2>
                    <div className={styles['form-group']}>
                        <label htmlFor="name">Name:</label>
                        <input type="text" id="name" name="name" value={editCompanyFormData?.name || ''} onChange={handleCompanyFormChange} required />
                    </div>
                    <div className={styles['form-group']}>
                        <label htmlFor="description">Description:</label>
                        <textarea id="description" name="description" value={editCompanyFormData?.description || ''} onChange={handleCompanyFormChange} />
                    </div>
                    <div className={styles['form-group']}>
                        <label htmlFor="email">Email:</label>
                        <input type="email" id="email" name="email" value={editCompanyFormData?.email || ''} onChange={handleCompanyFormChange} />
                    </div>
                    <div className={styles['form-group']}>
                        <label htmlFor="phoneNumber">Phone Number:</label>
                        <input type="tel" id="phoneNumber" name="phoneNumber" value={editCompanyFormData?.phoneNumber || ''} onChange={handleCompanyFormChange} />
                    </div>
                    <div className={styles['form-buttons']}>
                        <button type="submit" className={`action-button primary`} disabled={loading}>
                            {loading ? 'Saving...' : 'Save Changes'}
                        </button>
                        <button type="button" onClick={handleCancelEditCompany} className={`action-button secondary`} disabled={loading}>
                            Cancel
                        </button>
                    </div>
                </form>
            ) : (
                <>
                    <div className={styles['detail-header']}>
                        <h2 className={styles['section-header']}>{company.name}</h2>
                        <div className={styles['header-buttons']}>
                            <button onClick={handleEditCompanyClick} className={`${styles['action-button']} ${styles['edit-company-button']}`}>
                                Edit Company
                            </button>
                            <button onClick={handleDeleteCompany} className={`${styles['action-button']} ${styles['delete-company-button']}`}>
                                Delete Company
                            </button>
                        </div>
                    </div>

                    <div className={styles['detail-info']}>
                        <p><strong>Description:</strong> {company.description || 'N/A'}</p>
                        <p><strong>Email:</strong> {company.email || 'N/A'}</p>
                        <p><strong>Phone Number:</strong> {company.phoneNumber || 'N/A'}</p>
                    </div>

                    <h3 className={styles['section-subheader']}>Addresses</h3>
                    {company.addresses && company.addresses.length > 0 ? (
                        <ul className={styles['address-list']}>
                            {company.addresses.map((address, index) => (
                                <li key={index}>{address.addressName}</li>
                            ))}
                        </ul>
                    ) : (
                        <p className={styles['no-data-text']}>No addresses found for this company.</p>
                    )}

                    {/* Render the new CompanyDepartmentsSection component here */}
                    <CompanyDepartmentsSection
                        companyId={company.id}
                        departments={company.departments || []}
                        onDepartmentChange={handleDepartmentChange}
                    />
                </>
            )}

            <button onClick={() => navigate('/companies')} className={styles['back-button']}>
                Back to Companies List
            </button>
        </div>
    );
};

export default CompanyDetailsPage;