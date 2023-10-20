"use client";

import { AnimatePresence, LayoutGroup, motion } from "framer-motion";
import React from "react";
import { Loader } from "../(app)/loader";
import { AnimatedText } from "./(app)/animated-text";
import { ButtonAndLink } from "./(app)/button-and-link";
import { Mouse } from "./mouse";

export const Homepage = () => {
  const [position, setPosition] = React.useState({
    x: typeof window !== "undefined" ? window?.innerWidth / 2 : 0,
    y: 0,
  });

  const handleMouseMove = (event: React.MouseEvent) => {
    const { clientX, clientY } = event;
    setPosition({ x: clientX, y: clientY });
  };

  const [showLoader, setShowLoader] = React.useState<boolean>(true);
  const onChangeLoader = (finished: boolean) => {
    setShowLoader(finished);
  };

  return (
    <div className="-m-[34px] cursor-none">
      <LayoutGroup>
        <AnimatePresence>
          {showLoader ? (
            <Loader onChangeLoader={onChangeLoader} />
          ) : (
            <>
              <motion.div onMouseMove={handleMouseMove}>
                <Mouse r={50} x={position.x} y={position.y} />
              </motion.div>
              <div
                className="flex items-center justify-center flex-col relative overflow-hidden h-[calc(100vh-80px)] space-y-8 rounded-lg"
                onMouseMove={handleMouseMove}
              >
                <AnimatedText />
                <ButtonAndLink />
              </div>
            </>
          )}
        </AnimatePresence>
      </LayoutGroup>
    </div>
  );
};
