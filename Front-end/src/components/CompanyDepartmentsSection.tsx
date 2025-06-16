// src/components/CompanyDepartmentsSection.tsx
import React, { useState } from 'react';
import { Department, Employee, FoundEmployeeResult } from '../types/Types';
import DepartmentList from './DepartmentList'; // Your existing DepartmentList component
import styles from './CSSComponents/CompanyDetail.module.css'; // Re-use company detail styles

interface CompanyDepartmentsSectionProps {
    companyId: string;
    departments: Department[];
    onDepartmentChange: () => void; // Callback to re-fetch company details
}

const CompanyDepartmentsSection: React.FC<CompanyDepartmentsSectionProps> = ({ companyId, departments, onDepartmentChange }) => {
    // Employee search state
    const [searchFirstName, setSearchFirstName] = useState<string>('');
    const [searchLastName, setSearchLastName] = useState<string>('');
    const [foundEmployee, setFoundEmployee] = useState<FoundEmployeeResult | null>(null);
    const [isEmployeePopupVisible, setIsEmployeePopupVisible] = useState<boolean>(false);

    // Department search state
    const [searchDepartmentName, setSearchDepartmentName] = useState<string>('');
    const [searchDepartmentDescription, setSearchDepartmentDescription] = useState<string>('');
    const [foundDepartment, setFoundDepartment] = useState<Department | null>(null);
    const [isDepartmentPopupVisible, setIsDepartmentPopupVisible] = useState<boolean>(false);

    // Employee search handlers
    const handleSearchFirstNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchFirstName(e.target.value);
    };

    const handleSearchLastNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchLastName(e.target.value);
    };

    const handleSearchEmployee = () => {
        let foundEmployeeResult: FoundEmployeeResult | null = null;

        for (const dept of departments) {
            if (dept.employees) {
                for (const emp of dept.employees) {
                    const firstNameMatch = searchFirstName.toLowerCase().trim() === '' || emp.firstName.toLowerCase().trim().includes(searchFirstName.toLowerCase().trim());
                    const lastNameMatch = searchLastName.toLowerCase().trim() === '' || emp.lastName.toLowerCase().trim().includes(searchLastName.toLowerCase().trim());

                    if (firstNameMatch && lastNameMatch) {
                        foundEmployeeResult = {
                            firstName: emp.firstName,
                            lastName: emp.lastName,
                            email: emp.email,
                            position: emp.position,
                            departmentName: dept.name,
                        };
                        break; // Found employee, exit inner loop
                    }
                }
            }
            if (foundEmployeeResult) break; // Found employee, exit outer loop
        }

        if (foundEmployeeResult) {
            setFoundEmployee(foundEmployeeResult);
            setIsEmployeePopupVisible(true);
        } else {
            alert('No employee found with that first and last name in this company.');
            setFoundEmployee(null);
            setIsEmployeePopupVisible(false);
        }
    };

    const closeEmployeePopup = () => {
        setIsEmployeePopupVisible(false);
        setFoundEmployee(null);
    };

    // Department search handlers
    const handleSearchDepartmentNameChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchDepartmentName(e.target.value);
    };

    const handleSearchDepartmentDescriptionChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchDepartmentDescription(e.target.value);
    };

    const handleSearchDepartment = () => {
        const foundDept = departments.find(dept => {
            const nameMatch = searchDepartmentName.toLowerCase().trim() === '' || dept.name.toLowerCase().trim().includes(searchDepartmentName.toLowerCase().trim());
            const descriptionMatch = searchDepartmentDescription.toLowerCase().trim() === '' || (dept.description || '').toLowerCase().trim().includes(searchDepartmentDescription.toLowerCase().trim());
            return nameMatch && descriptionMatch;
        });

        if (foundDept) {
            setFoundDepartment(foundDept);
            setIsDepartmentPopupVisible(true);
        } else {
            alert('No department found with that name and description in this company.');
            setFoundDepartment(null);
            setIsDepartmentPopupVisible(false);
        }
    };

    const closeDepartmentPopup = () => {
        setIsDepartmentPopupVisible(false);
        setFoundDepartment(null);
    };

    return (
        <div className={styles['departments-section']}>
            <h3 className={styles['section-subheader']}>Departments</h3>

            {/* Search Bar for Employee by First and Last Name */}
            <div className={styles['search-bar']}>
                <input
                    type="text"
                    placeholder="Employee First Name"
                    value={searchFirstName}
                    onChange={handleSearchFirstNameChange}
                    className={styles['search-input']}
                />
                <input
                    type="text"
                    placeholder="Employee Last Name"
                    value={searchLastName}
                    onChange={handleSearchLastNameChange}
                    className={styles['search-input']}
                />
                <button onClick={handleSearchEmployee} className={styles['search-button']}>
                    Search Employee
                </button>
            </div>

            {/* Search Bar for Department by Name and Description */}
            <div className={styles['search-bar']}>
                <input
                    type="text"
                    placeholder="Department Name"
                    value={searchDepartmentName}
                    onChange={handleSearchDepartmentNameChange}
                    className={styles['search-input']}
                />
                <input
                    type="text"
                    placeholder="Department Description"
                    value={searchDepartmentDescription}
                    onChange={handleSearchDepartmentDescriptionChange}
                    className={styles['search-input']}
                />
                <button onClick={handleSearchDepartment} className={styles['search-button']}>
                    Search Department
                </button>
            </div>

            {departments.length === 0 ? (
                <p className={styles['no-data-text']}>No departments found for this company.</p>
            ) : (
                <DepartmentList
                    companyId={companyId}
                    departments={departments}
                    onDepartmentChange={onDepartmentChange}
                />
            )}

            {/* Pop-up for displaying found employee */}
            {isEmployeePopupVisible && foundEmployee && (
                <div className={styles['popup-overlay']}>
                    <div className={styles['popup-content']}>
                        <h3 className={styles['popup-header']}>Found Employee</h3>
                        <p><strong>Name:</strong> {foundEmployee.firstName} {foundEmployee.lastName}</p>
                        <p><strong>Email:</strong> {foundEmployee.email}</p>
                        <p><strong>Position:</strong> {foundEmployee.position}</p>
                        <p><strong>Department:</strong> {foundEmployee.departmentName}</p>
                        <button onClick={closeEmployeePopup} className={styles['popup-close-button']}>Close</button>
                    </div>
                </div>
            )}

            {/* Pop-up for displaying found department */}
            {isDepartmentPopupVisible && foundDepartment && (
                <div className={styles['popup-overlay']}>
                    <div className={styles['popup-content']}>
                        <h3 className={styles['popup-header']}>Found Department</h3>
                        <p><strong>Name:</strong> {foundDepartment.name}</p>
                        <p><strong>Description:</strong> {foundDepartment.description || 'N/A'}</p>
                        <p><strong>Email:</strong> {foundDepartment.email || 'N/A'}</p>
                        <p><strong>Phone Number:</strong> {foundDepartment.phoneNumber || 'N/A'}</p>
                        {foundDepartment.type && <p><strong>Type:</strong> {foundDepartment.type}</p>}
                        {/* Display addresses if department has them */}
                        {foundDepartment.addresses && foundDepartment.addresses.length > 0 && (
                            <>
                                <p><strong>Addresses:</strong></p>
                                <ul className={styles['address-list']}>
                                    {foundDepartment.addresses.map((address, index) => (
                                        <li key={index}>{address.addressName}</li>
                                    ))}
                                </ul>
                            </>
                        )}
                        <button onClick={closeDepartmentPopup} className={styles['popup-close-button']}>Close</button>
                    </div>
                </div>
            )}
        </div>
    );
};

export default CompanyDepartmentsSection;