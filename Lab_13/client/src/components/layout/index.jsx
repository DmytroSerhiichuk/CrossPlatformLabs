import { Outlet } from "react-router-dom";
import Header from "./header";

const Layout = () => {
  return (
		<>
      <Header />
      <main className="p-3 w-75 container">
        <Outlet />
      </main>
    </>
	);
}

export default Layout;