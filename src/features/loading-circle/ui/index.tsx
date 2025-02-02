import { CircularProgress } from "@mui/material";
import styles from "./index.module.css";

export const LoadingCircle = () => {
  return (
    <>
      <div className={styles.loadingContainer}>
        <CircularProgress size="3rem" />
      </div>
    </>
  );
};
