2015/10/11
Currently, using websocket to send the data to the client by json. The data format is like this:{"id":"1","px":"137.787","py":"107.770"}.
And the client can move the marker based on these data.

Issues:
1. Need to find out the strategy for re-link, no responses link.
2. Need to code a better algorithm for updating markers' location and add markers.