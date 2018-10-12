import React from "react";
import { Platform, StyleSheet, Image } from "react-native";
import { Constants, Location, Permissions, MapView, View } from "expo";

import gigantosaurus from "../assets/images/gigant.jpg";

export default class MapScreen extends React.Component {
  static navigationOptions = {
    title: "Map"
  };

  state = {
    location: {
      latitude: 50,
      longitude: 50
    }
  };

  componentWillMount() {
    if (Platform.OS === "android" && !Constants.isDevice) {
      this.setState({
        errorMessage:
          "Oops, this will not work on Sketch in an Android emulator. Try it on your device!"
      });
    } else {
      this._getLocationAsync();
    }
  }

  _getLocationAsync = async () => {
    await Permissions.askAsync(Permissions.LOCATION);

    let location = await Location.getCurrentPositionAsync({});
    this.setState({ location: location.coords });
  };

  render() {
    console.log(this.state);
    return (
      <MapView
        style={{ flex: 1 }}
        region={{
          latitude: this.state.location.latitude,
          longitude: this.state.location.longitude,
          latitudeDelta: 0.0922,
          longitudeDelta: 0.0421
        }}
      >
        <MapView.Marker
          coordinate={{
            latitude: 50.278248973370616,
            longitude: 18.685178042100794
          }}
          title="Someone"
          description="test"
        >
        <Image
            source={{uri: 'https://scontent-frt3-1.xx.fbcdn.net/v/t1.0-9/43952857_100993130890666_8154959013138661376_n.jpg?_nc_cat=102&oh=403e2a2de08e410fcd5216f848b1bd10&oe=5C4A5B92'}} style={styles.user}
          />
        </MapView.Marker>
      </MapView>
    );
  }
}

const styles = StyleSheet.create({
  user: {
    width: 50,
    height: 50,
    borderRadius: 25
  }
});
