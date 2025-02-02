import { RouterProvider } from "react-router-dom";
import { Router } from "./routing";

export const App = () => {
  return <RouterProvider router={Router} />;
};
