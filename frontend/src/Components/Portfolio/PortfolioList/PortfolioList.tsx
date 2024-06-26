import { PortfolioGet } from '../../../Models/Portfolio';
import PortfolioCard from '../PortfolioCard/PortfolioCard';
import './PortfolioList.css'

import React, { SyntheticEvent } from 'react'

interface Props {
  portfolio: PortfolioGet[];
  onPortfolioDelete: (e: SyntheticEvent) => void;
}

const PortfolioList = (props: Props) => {
  return (
    <section id="portfolio">
      <h2 className="mb-3 mt-3 text-3xl font-semibold text-center md:text-4xl">
        My Portfolio
      </h2>
      <div className="relative flex flex-col items-center max-w-5xl mx-auto space-y-10 px-10 mb-5 md:px-6 md:space-y-0 md:space-x-7 md:flex-row">
          {props.portfolio.length > 0 ? (
            props.portfolio.map((value) => {
              return <PortfolioCard portfolioValue={value} onPortfolioDelete={props.onPortfolioDelete} />
            })
          ) : (
            <h3 className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
              Your portfolio is empty.
            </h3>
          )}
      </div>
    </section>
  )
}

export default PortfolioList