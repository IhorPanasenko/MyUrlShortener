import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./pages/login/Login";
import ShortUrlsTable from "./pages/shortUrlsTable/ShortUrlsTable";
import ShortUrlInfo from "./pages/shortUrlInfo/ShortUrlInfo";
import AboutPage from "./pages/AboutPage/AboutPage";
import './App.css'

function App() {
  return (
    <>
      <div className="d-flex flex-column min-vh-100">
        <Router>
          <Routes>
            <Route path="/" Component={Login} />
            <Route path="/ShortUrlsTable" Component={ShortUrlsTable} />
            <Route path="/ShortUrlInfo/:urlId" Component={ShortUrlInfo} />
            <Route path="/AboutPage" Component={AboutPage} />
          </Routes>
        </Router>
      </div>
    </>
  );
}

export default App;
