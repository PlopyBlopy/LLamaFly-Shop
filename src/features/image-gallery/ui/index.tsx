import {
  useState,
  useCallback,
  useEffect,
  useMemo,
  forwardRef,
  useRef,
} from "react";
import { IconButton } from "@mui/material";
import { ArrowBackIos, ArrowForwardIos } from "@mui/icons-material";
import styles from "./index.module.css";

interface ImageGalleryProps {
  images: Map<number, string>;
}

export const ImageGallery = ({ images }: ImageGalleryProps) => {
  const [currentIndex, setCurrentIndex] = useState(0);
  const [nextIndex, setNextIndex] = useState<number | null>(null);
  const [direction, setDirection] = useState<"left" | "right">("right");

  const imagesArray = useMemo(() => {
    return Array.from(images.entries())
      .filter(([key]) => key >= 0)
      .sort((a, b) => a[0] - b[0])
      .map((entry) => entry[1]);
  }, [images]);

  const thumbnailRefs = useRef<(HTMLDivElement | null)[]>([]);
  useEffect(() => {
    const scrollToThumbnail = () => {
      if (thumbnailRefs.current[currentIndex]) {
        thumbnailRefs.current[currentIndex]?.scrollIntoView({
          behavior: "smooth",
          inline: "center",
          block: "nearest",
        });
      }
    };

    scrollToThumbnail();
  }, [currentIndex]);

  const startTransition = useCallback(
    (newIndex: number) => {
      if (newIndex === currentIndex) return;

      setDirection(newIndex > currentIndex ? "right" : "left");
      setNextIndex(newIndex);
    },
    [currentIndex]
  );

  const handleThumbnailClick = useCallback(
    (newIndex: number) => {
      startTransition(newIndex);
    },
    [startTransition]
  );

  const handleNext = useCallback(() => {
    startTransition((currentIndex + 1) % imagesArray.length);
  }, [currentIndex, imagesArray.length, startTransition]);

  const handlePrev = useCallback(() => {
    startTransition(
      (currentIndex - 1 + imagesArray.length) % imagesArray.length
    );
  }, [currentIndex, imagesArray.length, startTransition]);

  useEffect(() => {
    if (nextIndex === null) return;

    const timer = setTimeout(() => {
      setCurrentIndex(nextIndex);
      setNextIndex(null);
    }, 500);

    return () => clearTimeout(timer);
  }, [nextIndex]);

  if (imagesArray.length === 0) return null;

  const getSlideClass = (type: "current" | "next") => {
    if (nextIndex === null) return styles.slideStatic;

    if (direction === "right") {
      return type === "current"
        ? `${styles.slide} ${styles.slideOutLeft}`
        : `${styles.slide} ${styles.slideInFromRight}`;
    }

    return type === "current"
      ? `${styles.slide} ${styles.slideOutRight}`
      : `${styles.slide} ${styles.slideInFromLeft}`;
  };

  return (
    <div className={styles.container}>
      <div className={styles.slidesWrapper}>
        {/* Current Slide */}
        <img
          className={getSlideClass("current")}
          src={imagesArray[currentIndex]}
          alt={`Slide ${currentIndex + 1}`}
          style={{ zIndex: 2 }}
        />

        {/* Next Slide */}
        {nextIndex !== null && (
          <img
            className={getSlideClass("next")}
            src={imagesArray[nextIndex]}
            alt="Next slide"
            style={{ zIndex: 1 }}
          />
        )}

        {/* Navigation Buttons */}
        <IconButton
          onClick={handlePrev}
          aria-label="Previous image"
          sx={buttonStyles.left}>
          <ArrowBackIos fontSize="inherit" />
        </IconButton>

        <IconButton
          onClick={handleNext}
          aria-label="Next image"
          sx={buttonStyles.right}>
          <ArrowForwardIos fontSize="inherit" />
        </IconButton>
      </div>

      {/* Thumbnails */}
      <div className={styles.thumbnailsContainer}>
        {imagesArray.map((img, index) => (
          <Thumbnail
            key={index}
            ref={(el) => (thumbnailRefs.current[index] = el)}
            img={img}
            index={index}
            isActive={index === currentIndex}
            onClick={handleThumbnailClick}
          />
        ))}
      </div>
    </div>
  );
};

const Thumbnail = forwardRef<
  HTMLDivElement,
  {
    img: string;
    index: number;
    isActive: boolean;
    onClick: (index: number) => void;
  }
>(({ img, index, isActive, onClick }, ref) => (
  <div
    ref={ref}
    className={`${styles.thumbnailWrapper} ${
      isActive ? styles.activeThumbnailWrapper : ""
    }`}
    onClick={() => onClick(index)}>
    <div className={styles.thumbnail}>
      <img
        src={img}
        alt={`Thumbnail ${index + 1}`}
        className={`${styles.thumbnailImage} ${
          !isActive ? styles.inactiveThumbnail : ""
        }`}
      />
    </div>
  </div>
));

const buttonStyles = {
  left: {
    position: "absolute",
    left: "16px",
    top: "50%",
    transform: "translateY(-50%)",
    zIndex: 3,
    backgroundColor: "rgba(255, 255, 255, 0.8)",
    "&:hover": { backgroundColor: "rgba(0, 0, 0, 0.1)" },
  },
  right: {
    position: "absolute",
    right: "16px",
    top: "50%",
    transform: "translateY(-50%)",
    zIndex: 3,
    backgroundColor: "rgba(255, 255, 255, 0.8)",
    "&:hover": { backgroundColor: "rgba(0, 0, 0, 0.1)" },
  },
};
