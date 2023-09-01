import React, { useState, useEffect } from 'react';
import Header from "../../components/header/Header";
import Footer from "../../components/footer/Footer";
import axios from 'axios';
import jwtDecode from 'jwt-decode';

const URL_GET_DESCRIPTION = "https://localhost:7097/api/AboutPage/GetPageDescription"
//const URL_UPDATE_DESCRIPTION = "https://localhost:7097/api/AboutPage/Update?description="
const URL_UPDATE_DESCRIPTION = "https://localhost:7097/api/AboutPage/Update"

function AboutPage() {
    const [description, setDescription] = useState('');
    const [newDescription, setNewDescription] = useState('');
    const [userRole, setUserRole] = useState('');


    useEffect(() => {
        // Fetch the initial description
        const token = localStorage.getItem("token");
        if (token) {
            const decoded = jwtDecode(token);
            const role = decoded.UserRole;
            setUserRole(role);
        }

        axios.get(URL_GET_DESCRIPTION)
            .then(response => {
                setDescription(response.data);
            })
            .catch(error => {
                console.error(error);
            });
    }, []);

    const handleDescriptionUpdate = () => {
        if (userRole === 'Admin') {
            const token = localStorage.getItem("token");
            console.log(token);
            axios.put(URL_UPDATE_DESCRIPTION, `${newDescription}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            })
                .then(response => {
                    setDescription(newDescription);
                    setNewDescription('');
                })
                .catch(error => {
                    console.error(error);
                });
        }
    };

    return (
        <>
            <Header />
            <div className="d-flex flex-column justify-content-between flex-grow-1">
            <div className="container mt-5 fs-3">
                <h2>Shorting Algorithm</h2>
                <p>{description}</p>

                {userRole === 'Admin' && (
                    <div className="mt-4 border border-solid border-dark border-3 rounded p-2 bg-warning">
                        <h3>Edit Description (Admin Only)</h3>
                        <textarea
                            className="form-control fs-3"
                            rows="5"
                            value={newDescription}
                            onChange={e => setNewDescription(e.target.value)}
                        />
                        <button
                            className="fs-3 btn btn-primary mt-3 p-5 pt-3 pb-3"
                            onClick={handleDescriptionUpdate}
                        >
                            Update Description
                        </button>
                    </div>
                )}
            </div>
            <Footer />
            </div>
        </>
    );
}

export default AboutPage;
