"use client";

import { motion } from "framer-motion";

const variants = {
  hidden: {
    opacity: 0,
  },
  visible: {
    opacity: 1,
  },
};

export const AnimatedText = () => {
  return (
    <div className="-mt-16 flex items-center flex-col space-y-5">
      <motion.h1
        // initial={{ y: -100, opacity: 0 }}
        animate={{
          y: [-100, 0],
          opacity: 1,
          transition: {
            duration: 1,
          },
        }}
        className="text-5xl gap-3 flex font-silkscreen"
      >
        Welcome to Ploeg!
      </motion.h1>
      <motion.p className="font-extralight w-2/3 text-center">
        A place where all charge stations find its home. Empowering the future of mobility with
        clean energy without damaging the environment.
      </motion.p>
    </div>
  );
};
