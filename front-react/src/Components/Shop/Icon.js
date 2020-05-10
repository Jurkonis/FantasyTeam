import React, { Component } from "react";
import { connect } from "react-redux";
import { buyIcon } from "../../actions/shopActions.js";
import PropTypes from "prop-types";

export class Icon extends Component {
  handleBuy() {
    this.props.buyIcon(this.props.icon.id);
  }

  render() {
    let icon = this.props.icon;
    const logo = require("../../Images/Icons/" + icon.name + ".png");
    return (
      <div className="icon">
        <div className="iconImg">
          <img src={logo} alt="icon" />
        </div>
        <div className="price">{icon.price} coins</div>
        <div className="myButton3" onClick={this.handleBuy.bind(this)}>
          Buy
        </div>
      </div>
    );
  }
}

Icon.propTypes = {
  buyIcon: PropTypes.func.isRequired,
};

const mapStateToProps = (state) => ({});

export default connect(mapStateToProps, { buyIcon })(Icon);
