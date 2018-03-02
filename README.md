# Higgs
A generic dashboard for viewing and providing feedback to SOBotics bots.

## The goal

- Consolidate and create a consistent dashboard for all bots
- Make it as simple as possible for a bot to register detected content, and track their heuristics
- Consolidate the bot reviewing experience. Users should be able to handle feedback from any bot, without having to switch contexts
- To provide a standard API for those wanting to interact with, or analyze data detected by bots

## How to achieve it?

- Create a somewhat dumb backend, which is *primarily* a CRUD interface. Bots detect a post, send as much detail to the API as they feel necessary, and the Higgs front-end does its best to present it in a meaningful way.
- Implement the API to be as flexible as possible; almost every field used by the bots is optional.
- The API is supported by [swagger](https://swagger.io/) which allows bots to generate all the code required to make requests to Higgs. No more boilerplate! 

    Here's an example being used in the front-end. Everything else was automatically generated:
    ```typescript
    this.reviewerService.reviewerReportGet(reportId).subscribe(response => {
        this.postDetails = response;
    });
    ```
## What does the workflow look like?

- The bot owner generates a public/private key pair, and provides the public key to a Higgs admin.
- The admin registers a bot in Higgs, and sets up what feedback this bot is looking for (true positive, false positive, etc). Note that these reasons are unique *per bot*.
- Setup is done. From here, there's only one API endpoint important for the bot: `/Bot/RegisterPost`. For example, a request made by Heat Detector may look like this: (based on the report in chat [here](https://chat.stackoverflow.com/transcript/message/41446980#41446980)):

    ```json
    {
        "title": "new Answer(); Can this be considered as an object? [on hold]",
        "contentUrl": "https://stackoverflow.com/questions/49031290/new-answer-can-this-be-considered-as-an-object/49031322#comment85097884_49031322",
        "detectionScore": 6,
        "content": "Flagged it huh? Never down vote others answer if you don't follow your own rules, keep this in mind. This answer is useless for sure and it's not going to help future readers. Be the man of your own words",
        "authorName": "roundAbout",
        "contentCreationDate": "2018-03-01T09:40:30Z",
        "detectedDate": "2018-03-01 09:41:27Z",
        "reasons": [
            {
                "reasonName": "OpenNLP",
                "tripped": true,
                "confidence": 1
            }, 
            {
                "reasonName": "NaiveBayes",
                "tripped": false,
                "confidence": 0.96
            },
            { 
                "reasonName": "Perspective", 
                "tripped": false,
                "confidence": 0.39
            }
        ],
    }
    ```

    Here, the only required fields are `title` and `contentUrl`. The rest is optional, and entirely depends on the bot. The UI will render the report with whatever information is provided.


    The above example (slightly tweaked) can be seen [here](http://45.77.238.226:5555/report/2). Screenshot:

    ![Image for posterity](https://i.imgur.com/GXQibZN.png)

    A request by Natty may look like (report [here](https://chat.stackoverflow.com/transcript/message/41457261#41457261)):

    ```json
    {
        "title": "Were you able to find a solution?",
        "contentUrl": "https://stackoverflow.com/questions/44743352/how-to-create-a-virtual-serial-port-on-mac/49060496#49060496",
        "detectionScore": 6.5,
        "content": "Were you able to find a solution? I have the same issue",
        "authorName": "David",
        "contentCreationDate": "2018-03-01T23:35:59Z",
        "detectedDate": "2018-03-01 23:36:23Z",
        "reasons": [
            {
                "reasonName": "Contains ?"
            }, 
            {
                "reasonName": "No Code Block"
            },
            { 
                "reasonName": "Low Rep",
            }, 
            {
                "reasonName": "Unregistered User"
            }
        ],
    }
    ```

## Notes

This is currently the MVP product. The initial release will handle reports from bots, displaying reports, and recieving feedback from users. From there, we can build out the backend to provide:

- Automatic statistics and breakdowns of heuristics per bot (or globally)
- Pretty graphs and charts for free
- A consistent API for end users 
    - This means that writing a tool for Higgs (say, a UserScript like [AdvancedFlagging](https://github.com/SOBotics/AdvancedFlagging)) does not need to query multiple sources. 
    - As a bonus, this approach allows us to have bots *automatically integrated* with existing tools. New bots can be added to Higgs at any time, and have them just work with any tool/userscript.
- Displaying multiple bot reports on a single post
- Implementing a 'review queue'
- Allowing bots to query their heuristics (very useful for machine learning)

## Concerns

What do we do when a bot needs very specific logic? Ideally, we'd be able to generalize the problem, and add support for it in Higgs. However, that may not always be the case.

There are some options:

- If it's an issue with *displaying* the data, we can easily create a custom front-end without having to touch the server
- If it's an issue with *manipulating* or *storing* the data, we can stand up a new server based on the Higgs code with a few tweaks. This can be done in a few ways:
    - Git submodule (quick and easy, no issues with package managers)
    - Packages (NuGet)
    - Forking the repository
    
