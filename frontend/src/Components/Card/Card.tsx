import { SyntheticEvent } from 'react';
import { CompanySearch } from '../../FinModAPI/CompanyModels';
import AddPortfolio from '../Portfolio/AddPortfolio/AddPortfolio';
import './Card.css'
import { Link } from 'react-router-dom';

interface Props {
  id: string;
  company: CompanySearch;
  onPortfolioAdd: (e: SyntheticEvent) => void;
}

const Card = (props: Props) => {
  return (
    <div id={props.id} key={props.id} className="flex flex-col items-center justify-between w-full p-6 bg-slate-100 rounded-lg md:flex-row">
      <Link to={`/company/${props.company.symbol}/company-profile`} className="font-bold text-center text-black md:text-left">
        {props.company.name} ({props.company.symbol})
      </Link>
      <p className="text-black">
        {props.company.currency}
      </p>
      <p className="font-bold text-black">
        {props.company.exchangeShortName} - {props.company.stockExchange}
      </p>
      <AddPortfolio onPortfolioAdd={props.onPortfolioAdd} symbol={props.company.symbol} />
    </div>
  )
}

export default Card