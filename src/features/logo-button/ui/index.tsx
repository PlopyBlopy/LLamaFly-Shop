import { IconButton } from "@mui/material";
import styles from "./index.module.css";
type Props = {
  onPageOpen: () => void;
};

export const LogoButton = ({ onPageOpen }: Props) => {
  return (
    <>
      <IconButton onClick={onPageOpen} disableRipple>
        <img
          src="/logo/LlamaFly-full-ver1-logo.png"
          alt="Logo"
          className={styles.logoImage}
        />
      </IconButton>
    </>
  );
};
