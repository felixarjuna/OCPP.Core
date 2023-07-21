import { motion } from "framer-motion";
import React from "react";

const headerVariant = {
  initial: {
    y: -200,
  },
  animate: {
    y: 0,
    transition: {
      delayChildren: 0.4,
      staggerChildren: 0.1,
    },
  },
};

const Typewriter = ({ words }: { words: string[] }) => {
  const speed = 100;
  const scrollAt = 20;

  const textRef = React.useRef<HTMLDivElement>(null);

  React.useEffect(() => {
    let index = 0;
    let textPos = 0;
    let contents = "";
    let row: number;

    const typewriter = () => {
      contents = "";
      row = Math.max(0, index - scrollAt);

      if (textRef.current) {
        textRef.current.innerHTML =
          contents + words[index].substring(0, textPos) + `<span class="blink-caret">_</span>`;
      }

      if (textPos++ === words[index].length) {
        textPos = 0;
        index++;

        if (index !== words.length) {
          setTimeout(typewriter, 500);
        }
      } else {
        setTimeout(typewriter, speed);
      }
    };

    typewriter();
  }, []);

  return (
    <motion.h1
      ref={textRef}
      className="text-6xl font-silkscreen whitespace-nowrap overflow-hidden"
      variants={headerVariant}
      initial={"initial"}
      animate={"animate"}
    />
  );
};

export default Typewriter;
