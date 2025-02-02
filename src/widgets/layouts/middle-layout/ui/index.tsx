import { Container } from "@mui/material";
import { Outlet } from "react-router-dom";

export const MiddleLayout = () => {
  return (
    <Container>
      <Outlet />
    </Container>
  );
};
