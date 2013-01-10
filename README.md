AzureConnectDnsSync
===================
I have an on premise Active Directory server with an ADFS 2.0 server hosted in Windows Azure connected by Azure Connect. The Azure Connect IPv6 address will periodically change for one reason or another. This results in ADFS 2.0 being unable to contact the on premise Active Directory server to authenticate users.

This utility periodically checks if the Azure Connect IPv6 address has changed and updates the IPv6 DNS records accordingly.