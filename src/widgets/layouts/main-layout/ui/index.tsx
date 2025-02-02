import { Container, Box, CssBaseline } from "@mui/material";

import { MiddleLayout } from "../../middle-layout";
import { HeaderLayout } from "../../header-layout";
import { FooterLayout } from "../../footer-layout";

import styles from "./index.module.css";

export const MainLayout = () => {
  return (
    <Box className={styles.layout}>
      <CssBaseline />
      {/* Header */}
      <Box component="header" className={styles.header}>
        <HeaderLayout />
      </Box>
      {/* Content */}
      <Box component="main">
        <MiddleLayout />
      </Box>
      {/* Footer */}
      <Box component="footer" className={styles.footer}>
        <Container>
          <FooterLayout />
        </Container>
      </Box>
    </Box>
  );
};
