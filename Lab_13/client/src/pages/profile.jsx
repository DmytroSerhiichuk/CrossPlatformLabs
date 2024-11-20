import axios from "axios";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useAuth } from "../components/auth/AuthProvider";

const Profile = () => {
  const [user, setUser] = useState(false);
  const { logout, setIsAuthenticated } = useAuth();

  const fetchData = async () => {
    try {
      const res = await axios.get('http://localhost:3001/api/account/profile');
      console.log(res);
      setUser(res.data);
    }
    catch (err) {
      console.error(err);
      setIsAuthenticated(false);
    }
  }

  useEffect(() => {
    fetchData()
  }, []);

  const handleLogOut = async () => {
    logout();
  }

  if (user) {
    return (
      <div className="container">
        <div className="container mb-3">
          <h1 className="display-4 pb-3">Профіль</h1>
          <img src={user.picture} width="100" />
          <p>UserName - {user.userName}</p>
          <p>FullName - {user.fullName}</p>
          <p>Email - {user.email}</p>
          <p>Phone - {user.phone}</p>

          <button className="btn btn-primary" onClick={handleLogOut}>Log Out</button>
        </div>

        <h5>Оберіть лабораторно роботу:</h5>
        <ul className="navbar-nav">
          <li className="nav-item">
            <Link className="nav-link text-dark" to={'/lablib/lab-1'}>Lab 1</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link text-dark" to={'/lablib/lab-2'}>Lab 2</Link>
          </li>
          <li className="nav-item">
            <Link className="nav-link text-dark" to={'/lablib/lab-3'}>Lab 3</Link>
          </li>
        </ul>
      </div>
    );
  }
}

export default Profile;