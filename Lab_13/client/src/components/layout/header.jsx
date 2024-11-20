import { Link } from "react-router-dom";
import { useAuth } from "../auth/AuthProvider";

const Header = () => {
  const { isAuthenticated } = useAuth();
  
  return (
    <header>
      <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div className="container-fluid">
          <Link className="navbar-brand" to="/">Lab_13</Link>

          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
          </button>

          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav ms-auto">
              {!isAuthenticated ?
                <>
                  <li className="nav-item">
                    <Link className="nav-link" to="/account/login">Login</Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/account/signup">Sign Up</Link>
                  </li>
                </>
              :
              <>
                  <li className="nav-item">
                    <Link className="nav-link" to="/profile">Profile</Link>
                  </li>
                </>
              }
            </ul>
          </div>
        </div>
      </nav>
    </header>
  )
}

export default Header;