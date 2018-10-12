import React from 'react';
import { ScrollView, StyleSheet, Button } from 'react-native';
import { PrincipalService } from '../services/principal.service';

export default class LoginScreen extends React.Component {
    principalService = new PrincipalService();
    static navigationOptions = {
        title: 'Login',
    };

    async logIn() {
        const { type, token } = await Expo.Facebook.logInWithReadPermissionsAsync('2106060223020529', {
            permissions: ['public_profile'],
        });
        if (type === 'success') {
            // Get the user's name using Facebook's Graph API
            this.principalService.setFacebookToken(token);
            // const info = {
            //     method: 'post',
            //     headers: new Headers({
            //         'Content-Type': 'application/json'
            //     }),
            //     body: JSON.stringify({
            //         access_token: token
            //     })
            // }
            // const tokenRespone = await fetch('https://tinterest.azurewebsites.net/.auth/login/facebook', info);
            // const res = await tokenRespone.json()
            // const authenticationToken = res.authenticationToken;
            // console.log(authenticationToken);
            // this.principalService.setAccessToken(authenticationToken);

            // const requestData = {
            //     method: 'get',
            //     headers: new Headers({
            //         'x-zumo-auth': authenticationToken
            //     })
            // }

            // const response = await fetch(
            //     `https://tinterest.azurewebsites.net/api/TagsFunction?code=sEryH0snZBDJhtidZw4QomzOYhoHmGnvfA5QCfmfkelyttP4Ku6fsg==&name=TestUser`, requestData);
            // console.log(response);

            this.props.navigation.navigate('Home');
        }
    }

    render() {
        return (
            <ScrollView style={styles.container}>
                <Button onPress={() => this.logIn()} title="Login with Facebook!" />
            </ScrollView>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        paddingTop: 15,
        backgroundColor: '#fff',
    },
});
