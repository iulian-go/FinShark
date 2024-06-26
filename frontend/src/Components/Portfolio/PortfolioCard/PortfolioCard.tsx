import { Link } from 'react-router-dom';
import DeletePortfolio from '../DeletePortfolio/DeletePortfolio';
import './PortfolioCard.css'

import React, { SyntheticEvent } from 'react'
import { PortfolioGet } from '../../../Models/Portfolio';

interface Props {
  portfolioValue: PortfolioGet;
  onPortfolioDelete: (e: SyntheticEvent) => void;
}

const PortfolioCard = (props: Props) => {
  return (
    <div className="flex flex-col w-full p-8 space-y-4 text-center rounded-lg shadow-lg md:w-1/3">
      <Link to={`/company/${props.portfolioValue.symbol}/company-profile`} className="pt-6 text-xl font-bold">{props.portfolioValue.symbol}</Link>
      <DeletePortfolio ticker={props.portfolioValue.symbol} onPortfolioDelete={props.onPortfolioDelete} />
    </div>
  )
}

export default PortfolioCard