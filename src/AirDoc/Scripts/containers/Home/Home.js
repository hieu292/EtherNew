import React, { Component } from 'react';
import Helmet from 'react-helmet';
import { connect } from 'react-redux';
import { Carousel, CarouselItem } from 'react-bootstrap';

class Home extends Component {
  render() {
    return (
      <div>
        <Helmet title="Home" />
        <Carousel>
          <CarouselItem>
            <img
              src={require('./Banners-01.png')}
              alt="ASP.NET"
              className="img-responsive"
            />
            <div className="carousel-caption">
              <p>
                This application based on Ethereum - powerful blockchain technology.
                <a className="btn btn-default btn-default" href="https://www.ethereum.org/">
                  Read More
                </a>
              </p>
            </div>
          </CarouselItem>
          <CarouselItem>
            <img
              src={require('./Banners-02.png')}
              alt="Visual Studio"
              className="img-responsive"
            />
            <div className="carousel-caption">
              <p>
                This application build by strong, innovative, creative and professional team from TMA Solution.
                <a className="btn btn-default btn-default" href="http://www.tmasolutions.com/">
                  Read More
                </a>
              </p>
            </div>
          </CarouselItem>
          <CarouselItem>
            <img
              src={require('./Banners-03.png')}
              alt="Package Management"
              className="img-responsive"
            />
            <div className="carousel-caption">
              <p>
                Blockchain is a distributed database that maintains a continuously growing list of records, called blocks, secured from tampering and revision.
                <a className="btn btn-default btn-default" href="https://en.wikipedia.org/wiki/Blockchain">
                  Learn More
                </a>
              </p>
            </div>
          </CarouselItem>
          <CarouselItem>
            <img
              src={require('./Banners-04.png')}
              alt="Microsoft Azure"
              className="img-responsive"
            />
            <div className="carousel-caption">
              <p>
                TMA Solution always bring best solution for your business.
                <a className="btn btn-default btn-default" href="http://www.tmasolutions.com/">
                  Read More
                </a>
              </p>
            </div>
          </CarouselItem>
        </Carousel>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return state;
}

function mapDispatchToProps() {
  return {};
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Home);
