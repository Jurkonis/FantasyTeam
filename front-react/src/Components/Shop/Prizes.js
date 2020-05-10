import React, { Component } from "react";
import { connect } from "react-redux";
import { fetchShop } from "../../actions/shopActions.js";
import PropTypes from "prop-types";
import MessageList from "../Messages/MessagesList.js";
import Icon from "./Icon.js";

export class Prizes extends Component {
  componentDidMount() {
    this.props.fetchShop();
  }
  render() {
    let icons = this.props.shop.map((icon) => (
      <li key={icon.id}>
        <Icon id={icon.id} icon={icon} />
      </li>
    ));
    return (
      <div className="shopWrapper">
        <MessageList />
        <h2>Shop</h2>
        <ul className="shopList">{icons}</ul>
      </div>
    );
  }
}

Prizes.propTypes = {
  fetchShop: PropTypes.func.isRequired,
  shop: PropTypes.array.isRequired,
};

const mapStateToProps = (state) => ({
  shop: state.shop.shop,
});

export default connect(mapStateToProps, { fetchShop })(Prizes);
