import { RouterProvider } from "react-router-dom";
import { Router } from "../routing/index";

export const App = () => {
  return <RouterProvider router={Router} />;
};
