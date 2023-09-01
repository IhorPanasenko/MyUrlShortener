import React, { useState, useEffect } from 'react';
import Header from '../../components/header/Header';
import Footer from '../../components/footer/Footer';
import { useParams } from 'react-router-dom';
import axios from 'axios';

const URL_GET_URL_INFO = "https://localhost:7097/api/UrlLink/GetLinkInfo?linkId="

function ShortUrlInfo() {
  const { urlId } = useParams();
  const [linkInfo, setLinkInfo] = useState(null);
  const [error, setError] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem("token");

    if (!token) {
      setError(true);
      return;
    }

    const fetchLinkInfo = async () => {
      try {
        const response = await axios.get(`${URL_GET_URL_INFO}${urlId}`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });
        setLinkInfo(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchLinkInfo();
  }, [urlId]);

  const convertDate = (dateString) => {
    const dateObject = new Date(dateString);

    const options = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric' };
    const humanReadableDate = dateObject.toLocaleDateString('en-US', options);

    console.log(humanReadableDate);
    return humanReadableDate;
  }

  return (
    <>
      <Header />
      <div className='d-flex flex-column justify-content-between flex-grow-1'>
        <div className="container text-center mt-5 ">
          {error ? (
            <p className="alert alert-danger">Access denied</p>
          ) : linkInfo ? (
            <div className="fs-3 card">
              <div className="card-body bg-info border border-3 border-warning border-rounded">
                <h2 className="card-title">URL Information</h2>
                <p><strong>Long URL:</strong> {linkInfo.longUrl}</p>
                <p><strong>Short URL:</strong> {linkInfo.shortUrl}</p>
                <p><strong>Creation Date:</strong> {convertDate(linkInfo.creationDate)}</p>
                <p><strong>Description:</strong> {linkInfo.description}</p>
                <h3 className="mt-4">User Information</h3>
                <p><strong>User Name:</strong> {linkInfo.user.userName}</p>
                <p><strong>Email:</strong> {linkInfo.user.email}</p>
              </div>
            </div>
          ) : (
            <p>Loading...</p>
          )}
        </div>
        <Footer />
      </div>
    </>
  );
}

export default ShortUrlInfo;
