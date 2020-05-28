What are emotes?

Emotes can be seen as a scripting language inside of Asheron's Call.

Emotes let objects, such as NPCs, respond to events (such as the player double-clicking an object, or the player giving an object to an NPC) with a list of pre-defined actions.

Some of these actions can have branching logic: for example, if the player has War Magic skill trained, follow this path. If they don't, follow another path.

Here is an example emote table:

Use:
	- Tell: Hello there!
	- InqQuest: TestQuest
		QuestSuccess:
			- Tell: You have already started the TestQuest.
		QuestFailure:
			- SetQuestCompletions: TestQuest, 0
			- Tell: You have now started the TestQuest.
			
For this emote table, when the player uses the object by double-clicking it, the NPC will tell the player "Hello there!", and then check whether or not the player has started the TestQuest.

If the player has already started the quest, the NPC will tell the player "You have already started the TestQuest."

If the player has not already started the TestQuest, it will be added to their Quest Registry, and marked as 0 completions. The NPC will then tell the player "You have now started the TestQuest."

If you use Notepad++ and wish to have colors for the markup EmoteScript uses, you can use EmoteScript.xml in the EmoteScript\resources\ folder.  View the readme.txt in that directory on how to use.