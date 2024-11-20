import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "./AuthProvider";
import { useEffect } from "react";

const ProtectedRoute = ({ children }) => {
  const { isAuthenticated, checkAuth, isLoading } = useAuth();
  const location = useLocation();

  useEffect(() => {
    checkAuth();
  }, []);

  return isAuthenticated && !isLoading ? children : <Navigate to="/account/login" state={{ from: location }} />;
};

export default ProtectedRoute;