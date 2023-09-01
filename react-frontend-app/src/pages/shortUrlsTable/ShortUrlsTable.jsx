import React, { useState, useEffect } from 'react'
import Header from '../../components/header/Header'
import Footer from '../../components/footer/Footer'
import axios from 'axios';
import { Table, Button, Form } from 'react-bootstrap';
import { useNavigate, useLocation } from 'react-router-dom';
import jwt_decode from 'jwt-decode';
import validUrl from 'valid-url';

const URL_GET_ALL_LINKS = "https://localhost:7097/api/UrlLink/GetAll";
const URL_CREATE_LINK = "https://localhost:7097/api/UrlLink/Create";
const URL_GET_LONG_URL = "https://localhost:7097/api/UrlLink/GetLongUrl?shortUrl=";
const URL_DELETE_LINK = "https://localhost:7097/api/UrlLink/Delete?linkId=";

function ShortUrlsTable() {

    const [links, setLinks] = useState([]);
    const [longUrl, setLongUrl] = useState('');
    const [description, setDescription] = useState('');
    const [userRole, setUserRole] = useState('');
    const [userId, setUserId] = useState('');
    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            const decoded = jwt_decode(token)
            const role = decoded.UserRole
            setUserRole(role);

            const userId = decoded.UserId;
            setUserId(userId);
            console.log(userRole);
            console.log(userId);
        }
        else {
            setUserRole("NotAuthorized");
        }

        getLinks();
    }, []);

    const getLinks = async () => {
        const token = localStorage.getItem("token");
        await axios.get(URL_GET_ALL_LINKS, {
            headers: {
                Authorization: `Bearer ${token}`,
            }
        })
            .then((response) => {
                setLinks(response.data);
                console.log(response.data)
            })
            .catch((error) => {
                console.log(error);
            });
    }

    const handleCreateLinkClick = async (e) => {
        e.preventDefault();
        const token = localStorage.getItem("token");

        if (!validUrl.isWebUri(longUrl)) {
            alert("Please enter a valid URL link");
            return;
        }

        const data = {
            longUrl: longUrl,
            description: description,
            creationDate: new Date().toISOString(),
            userId: userId
        };

        console.log(data);

        await axios.post(URL_CREATE_LINK, data, {
            headers: {
                Authorization: `Bearer ${token}`,
            }
        })
            .then((response) => {
                getLinks();
                alert("Link created successfully");
                setDescription('');
                setLongUrl('');
            })
            .catch((error) => {
                console.log(error);
                if (error.response.data) {
                    alert(error.response.data);
                }
                else {
                    alert("Can't create new Link")
                }
            });
    }

    const handleViewDetailsClick = (urlId) => {
        navigate(`/ShortUrlInfo/${urlId}`)
    }

    const handleShortLinkClick = async (shortUrl) => {
        try {
            const response = await axios.get(`${URL_GET_LONG_URL}${shortUrl}`);
            const longUrl = response.data;
            console.log(longUrl);
            window.location.href = longUrl;
        } catch (error) {
            console.error(error);
            alert(error.response.data);
        }
    };

    const handleDeleteClicked = async (urlId) => {
        try {
            const token = localStorage.getItem("token");
            const response = await axios.delete(`${URL_DELETE_LINK}${urlId}&userId=${userId}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            });

            if (response.status === 200) {
                alert("Link deleted successfully");
                getLinks();
            } else {
                alert("Failed to delete link");
            }
        } catch (error) {
            console.error(error);
            alert("An error occurred while deleting the link");
        }
    };

    return (
        <>
            <Header />
            <div className="d-flex flex-column justify-content-between flex-grow-1">
                <div className="container">
                    {(userRole === "Admin" || userRole === "User") ? (
                        <Form className='fs-2 m-3 p-3 border border-solid border-3 border-dark bg-warning'>
                            <h2 className='text-center'>You can create your link</h2>
                            <div className="container d-flex flex-column justify-content-center">
                                <Form.Group className='mt-3' controlId="longUrl">
                                    <Form.Label>Long Url</Form.Label>
                                    <Form.Control className='fs-3' type="text" placeholder="Long Url" value={longUrl} onChange={(e) => setLongUrl(e.target.value)} />
                                </Form.Group>
                                <Form.Group className='mt-3' controlId="longUrl">
                                    <Form.Label>Description</Form.Label>
                                    <Form.Control className='fs-3' type="text" placeholder="Description" value={description} onChange={(e) => setDescription(e.target.value)} />
                                </Form.Group>
                            </div>
                            <div className="container d-flex justify-content-center mt-3">
                                <Button className='btn btn-success fs-2 p-5 pt-2 pb-2 mb-3' onClick={e => { handleCreateLinkClick(e) }}>Create Link</Button>
                            </div>
                        </Form>
                    ) : <div><h1>Links can create only registered users</h1></div>}
                </div>
                <Table className='fs-4 m-3 pb-5 mb-5' striped bordered hover>
                    <thead>
                        <tr>
                            <th>Long Url</th>
                            <th>Short Url</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {links?.map((link) => (
                            <tr key={link.urlId}>
                                <td><a href={link.longUrl}>{link.longUrl}</a></td>
                                <td><a href="#" className='pe-auto' onClick={() => handleShortLinkClick(link.shortUrl)}>{link.shortUrl}</a></td>
                                <td >
                                    {(userRole === "Admin" || userRole === "User") ? (
                                        <button type="button" className="fs-4 btn btn-primary border border-rounded p-5 pt-2 pb-2 m-2" onClick={() => handleViewDetailsClick(link.urlId)}>View Details</button>
                                    ) : <div>You can't view details</div>}
                                    {userRole === "Admin" || (userRole === "User" && userId === link.userId) ? (
                                        <button type="button" className="fs-4 btn btn-danger border border-rounded p-5 pt-2 pb-2 m-2" onClick={() => handleDeleteClicked(link.urlId)}>Delete</button>
                                    ) : <div>You can't delete this</div>}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
                <Footer />
            </div>
        </>
    )
}

export default ShortUrlsTable