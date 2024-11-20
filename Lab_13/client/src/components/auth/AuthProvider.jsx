import axios from "axios";
import { createContext, useContext, useEffect, useState } from "react";

const AuthContext = createContext();

const useAuth = () => useContext(AuthContext);

const AuthProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  const checkAuth = async () => {
    try {
      await axios.get('http://localhost:3001/api/account/check');
      setIsAuthenticated(true);
    }
    catch (err) {
      setIsAuthenticated(false);
    }
    finally {
      setIsLoading(false);
    }
  }

  useEffect(() => {
    checkAuth();
  }, []);

  const logout = async () => {
    try {
      await axios.post('http://localhost:3001/api/account/logout');
      setIsAuthenticated(false);
    }
    catch (err) {
      alert('Something went wrong');
      console.error(err);
    }
  }

  return (
    <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated, checkAuth, isLoading, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export { AuthProvider, useAuth }
